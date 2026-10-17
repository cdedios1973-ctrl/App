# 1. Gestión de Transacciones Maestro-Detalle

## Contexto
El DAO original instanciaba internamente el contexto de la base de datos por cada comando,
lo que impedía ejecutar operaciones multi-tabla (Maestro-Detalle) en una sola transacción
de forma segura.

## Decisión
Modificar la interfaz `IDAOCommands` para que reciba la abstracción `IDbExecutor` por parámetro.
De este modo, la capa de `Logic` puede gobernar el ciclo de vida de la transacción y
compartir el mismo contexto entre múltiples comandos nativos.

## Consecuencias
* Inversión de Dependencias y Responsabilidad Única.
* Eliminación de arrays bidimensionales de comandos en los constructores.
* Control total del Rollback/Commit desde la capa superior de negocio.