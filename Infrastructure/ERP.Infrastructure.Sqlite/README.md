# ERP.Infrastructure.Sqlite

Diseño e Implementación de la capa Persistencia con los Drivers Nativos de SqLite de Microsoft en ADO.NET.

------------------------------------------------------------------------

## 📂 Estructura del Proyecto

A continuación se detalla el propósito de cada carpeta en esta refactorización:

* **`Persistance/`**  				
     Definicion e implementacion de los accesos a base de datos R/W.
* **`InstallSqlite/`**					
     Autoinstalacion de bases de datos para entorno demo, leer ADR-005.


*** Detalles de estructuras en las que se esta trabajando actualmente:
----------------------------------------------------------------------
`Persistance/`
	- `Read/`	: Consultas de lectura contra la base de datos.
	- `Write/`  : Consultas de escritura conta la base de datos.

`InstallSqlite/` (Mantenerlo para ejecutar demo, Evaluar si es viable en aplicación monopuesto, para intranet mejor despligue manual)
	- Testeo de implementacion de base de datos con sus tablas, relaciones y datos semilla.