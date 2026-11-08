# ADR-0011: Copias de Seguridad de Base de Datos SQLite

## Estado
Aceptado

## Contexto
Se necesita implementar un sistema de copias de seguridad de la base de datos. Tras repasar los sistemas tradicionales e investigar las características específicas de SQLite, 
se ha determinado (con asistencia de IA) una solución para realizar los respaldos de forma segura y transparente. Esto no exime de cumplir adicionalmente con los procedimientos de almacenamiento en zonas seguras
 (servidores web, sistemas ignífugos, etc.) para total tranquilidad.

## Decisión
Implementar un Servicio de Copias de Seguridad dentro de la **Capa de Persistencia**. Este servicio podrá ser invocado de forma manual por el usuario o programado durante periodos de inactividad. 
Se ubica en esta capa debido a que `Microsoft.Data.Sqlite` expone métodos nativos específicos para realizar esta acción a bajo nivel sin comprometer la integridad.

## Consecuencias
### Positivas:
* **Código Limpio:** Al utilizar el espacio de nombres nativo `Microsoft.Data.Sqlite`, el mecanismo de respaldo se mantiene completamente desacoplado de la lógica de negocio.
* **Seguridad de Datos:** Se garantiza un sistema de copias de seguridad 100% consistente y seguro sin corromper el archivo original.
* **Operación en Caliente:** Permite realizar el respaldo mientras la aplicación está en uso sin necesidad de detener la operativa del terminal.

### Negativas / Costes:
* **Consumo de Almacenamiento:** Incremento en el uso del disco duro al duplicar el archivo de la base de datos en la ruta de destino.
* **Impacto en Rendimiento:** Durante el proceso de volcado de páginas de memoria, terminales con hardware muy limitado podrían experimentar una ligera degradación temporal en los tiempos de respuesta.

## Anexo Técnico
1. **El archivo WAL (Write-Ahead Log):** Si SQLite está configurada en modo WAL, las escrituras pendientes se encuentran en el archivo `.db-wal`. El método nativo se encarga de consolidar visualmente el estado correcto.
2. **Copias en Caliente de Alto Nivel:** A diferencia de una copia de bytes tradicional a nivel de sistema de archivos (que sufriría inconsistencia si coincide con una escritura), los creadores de SQLite diseñaron herramientas específicas para duplicar la base de datos de manera segura en ejecución:
   * **SQL Puro:** `VACUUM INTO '/ruta/de/tu/copia_seguridad.db';`
   * **API nativa de ADO.NET:** El método `BackupDatabase()`.

### Código de Ejemplo en C# (Adaptado con Inyección de Dependencias)

```csharp
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

public interface IBackupService
{
    Task RealizarCopiaSeguridadAsync(string rutaDestino);
}

public class BackupService : IBackupService
{
    private readonly string _connectionStringOrigen;

    // Se inyecta la cadena de conexión de producción configurada en la app
    public BackupService(string connectionStringOrigen)
    {
        _connectionStringOrigen = connectionStringOrigen;
    }

    public async Task RealizarCopiaSeguridadAsync(string rutaDestino)
    {
        var connectionStringDestino = new SqliteConnectionStringBuilder
        {
            DataSource = rutaDestino,
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();

        using (var conexionOrigen = new SqliteConnection(_connectionStringOrigen))
        using (var conexionDestino = new SqliteConnection(connectionStringDestino))
        {
            await conexionOrigen.OpenAsync();
            await conexionDestino.OpenAsync();

            // Ejecuta la copia a bajo nivel asegurando la consistencia
            conexionOrigen.BackupDatabase(conexionDestino);
        }
    }
}
```

---
* Redacción: Diseñado por el autor y optimizado para corregir inconsistencias técnicas en la operativa en caliente e incorporar buenas prácticas de arquitectura.*
