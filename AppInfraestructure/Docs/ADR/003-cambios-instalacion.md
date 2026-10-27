# ADR-003: Autodespliegue de Base de Datos Maestra mediante Scripts SQL

## Estado
Aceptado

## Contexto
El despliegue inicial creaba la base de datos, las tablas y los registros semilla codificados directamente en clases de C#. 
El sistema validaba la existencia física del archivo de base de datos antes de proceder con esta instalación mediante código.

## Decisión
Migrar la lógica de creación de esquemas y carga de datos semilla desde el código C# hacia **archivos de Scripts SQL externos (.sql)** 
embebidos o leídos por la aplicación.

## Consecuencias
* **Separación de responsabilidades:** El código de la aplicación se libera de la carga de estructurar la base de datos a nivel de texto plano.
* **Mantenibilidad:** Modificar el esquema o los datos semilla es más rápido, limpio y legible a través de archivos SQL estándar.
* **Trazabilidad:** Facilita el control de versiones (Git) de la evolución del esquema de la base de datos.