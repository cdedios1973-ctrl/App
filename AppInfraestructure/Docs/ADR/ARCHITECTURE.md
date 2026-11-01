# Registro de Decisión Arquitectónica (ADR): Flujo de Facturación Concurrente

## 1. Contexto y Problema
En un sistema multipuesto, la generación del número oficial de factura (`NumeroFactura`) es propensa a condiciones de carrera (race conditions). Dos puestos podrían calcular en memoria el mismo número correlativo simultáneamente, provocando duplicidad de claves o saltos en la numeración legal al persistir en la base de datos.

Modificar las entidades de la capa de **Lógica** desde la capa de **Persistencia** para inyectar el número final viola de forma estricta los principios **SOLID** (específicamente SRP y OCP), destruyendo la inmutabilidad y la pureza del dominio.

## 2. Decisión Arquitectónica
Se implementa una separación absoluta de modelos mediante el patrón de **DTO de Persistencia (Table Models)** y un mapeo unidireccional en el constructor de infraestructura. 

* **La Lógica de Negocio** calcula totales, impuestos y relaciones en memoria de forma pura e inmutable, ignorando la existencia de IDs de base de datos o numeraciones correlativas físicas.
* **La Persistencia** gestiona de forma atómica la transacción, bloquea la fila del contador en hardware, obtiene la "matrícula" legal y mapea el modelo de dominio a su propio modelo relacional mediante un constructor `1:1`.

## 3. Implementación del Código

### Capa de Dominio / Lógica (Inmutable)
```csharp
public class TicketDetail 
{
    public string Producto { get; }
    public decimal Precio { get; }
    public int Cantidad { get; }

    public TicketDetail(string producto, decimal precio, int cantidad)
    {
        Producto = producto;
        Precio = precio;
        Cantidad = cantidad;
    }
}
```

### Capa de Infraestructura / Persistencia (Modelos de Tabla)
```csharp
public class TicketDetailTable 
{
    // Propiedades exclusivas de restricciones de Base de Datos
    public int Id { get; set; } 
    public string NumeroFactura { get; set; } 

    // Propiedades de negocio mapeadas
    public string Producto { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    // Constructor 1:1 que protege los principios SOLID
    public TicketDetailTable(TicketDetail dominio, string numeroFactura)
    {
        this.Producto = dominio.Producto;
        this.Precio = dominio.Precio;
        this.Cantidad = dominio.Cantidad;
        this.NumeroFactura = numeroFactura; // Inyección de identidad transaccional
    }
}
```

## 4. Flujo Transaccional de Ejecución

```
[ Capa de Lógica ]                               [ Capa de Persistencia ]
  Genera Ticket (RAM) -------------------------> Recibe Objeto de Dominio
                                                            |
                                                   BEGIN TRANSACTION
                                                            |
                                                 Bloquea Tabla 'Counter'
                                                 Obtiene "FAC-2026-001"
                                                            |
                                                 Instancia TicketDetailTable
                                                 Mapea por constructor 1:1
                                                            |
                                                   Guarda en Tablas SQL
                                                            |
                                                       COMMIT
```

## 5. Consecuencias y Beneficios
* **Cumplimiento de SOLID:** La lógica está cerrada a modificaciones externas (OCP) y tiene un solo motivo de cambio (reglas de negocio, no de base de datos - SRP).
* **Seguridad Física y Concurrente:** El bloqueo se realiza a nivel de motor de base de datos dentro de una transacción en milisegundos. Cualquier fallo eléctrico o de red intermedio provoca un `ROLLBACK` automático, garantizando la integridad legal.
* **Desacoplamiento Absoluto:** Si el almacenamiento migra a un sistema NoSQL o archivos planos, la lógica permanece 100% intacta.

## 6. Notas de Diseño y Créditos
*Este documento refleja una decisión de diseño consolidada en un proceso de reciclaje y autoaprendizaje técnico autónomo, validando enfoques de concurrencia y el cumplimiento estricto de los principios SOLID a través de dinámicas de "rubber ducking" con un asistente de IA.*
