# ADR-0010: Segregacion de Repositorios de Escritura y Control de Transacciones Nativo

## Estado
Aceptado (Reemplaza al ADR-0002)

## Contexto
El intento previo de unificar transacciones maestro-detalle a través de una abstracción inyectada (`IDbExecutor`) fue rechazado debido a la complejidad sintáctica que añadía. 
Sin embargo, persistía la necesidad de asegurar operaciones de modificación de datos de forma segura, estructurada y atómica sin acoplar la lógica de negocio a la limpieza
manual de memoria (bloques `try/catch/finally` densos).

## Decisión
Implementar un enfoque inspirado en CQRS, aislando las operaciones de mutación de datos en repositorios exclusivos de escritura (Write Repositories) con las siguientes directrices técnicas:
1. **Encapsulamiento del Ensamblado:** Declarar clases como `SqliteUserWriteRepository` con visibilidad `internal` mediante relaciones de confianza entre ensamblados para blindar el acceso directo desde el exterior.
2. **Uso de Declaraciones 'using var':** Adoptar las declaraciones de variables `using var` de C# para la conexión, comandos y transacciones nativas de ADO.Net, delegando en el compilador la liberación de recursos en orden inverso.
3. **Control Atómico del Ciclo:** Mantener el `Commit()` y `Rollback()` autocontenido dentro del flujo del repositorio, asegurando un diseño determinista basado en el éxito de los comandos escalares o no-consultas (`ExecuteNonQuery`).

## Consecuencias
### Positivas:
* **Código Limpio e Idiomático:** Se eliminan decenas de líneas de código repetitivo de limpieza manual de punteros y conexiones (`Dispose()`).
* **Seguridad de Datos:** El recolector de basura y el entorno de ejecución garantizan el Rollback automático del recurso físico si la transacción es destruida antes de consolidar el cambio.
* **Modularidad:** Facilidad absoluta para escalar operaciones complejas en paralelo sin arrastrar un gestor de contexto global pesado.

### Negativas / Costes:
* Duplicación controlada de lógica de apertura de conexiones en métodos individuales a cambio de una total independencia y aislamiento de hilos (Thread-Safety).

        Redacción: Diseñado por el autor y optimizado con asistencia de IA para formato ADR. 