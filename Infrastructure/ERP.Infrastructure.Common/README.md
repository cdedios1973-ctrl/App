# ERP.Infrastructure.Common

Diseño e Implementación de una serie de funcionalidades basicas para consumir tanto en Infraestructura, Logica.

------------------------------------------------------------------------

## 📂 Estructura del Proyecto

A continuación se detalla el propósito de cada carpeta en esta refactorización:

* **`Deprecated/`**  
  Código previo, comentado y ya refactorizado.

* * **`Common/`**  
  Coleccion de herramientas básicas de la aplicación.
	- `Interfaces/`   : Definiciones de los contratos de uso interno.
	- `XMLReader/`    : XML para leer documentos internos de configuracion / uso.
	- `Installation/` : Autoinstalacion de bases de datos para entorno demo, leer ADR-005.

* * **`Services/`**  
  Definicion de los servicios que expone este ensamblado (WIP)


### Estructura del archivo de configuración 'appsettings.json'

{
  "ConnectionStrings": { "SqliteConnection": "Data Source=AppDemo.db;" }, Aqui se define la cadena de conexión explicita y completa.

  "DatabaseName":			"AppDemo",								-> Nombre de la base de datos, Debe coincidir con el expuesto en la cadena de conexion.
  "LocalDatabasePath":		"Database",								-> Nombre de la carpeta contenedora de la base de datos a partir de la ruta del ejecutable
  "NetworkDatabasePath":	"S:\\ExamplePath\\Database\\",			-> (WIP) Nombre de la carpeta en red local contenedora de la base de datos.
  "LocalXMLPath":			"XMLDocs"								-> Nombre de la carpeta contenedora de los archivos XML, de configuracion.
}


*** Detalles de estructuras en las que se esta trabajando actualmente:
----------------------------------------------------------------------
`Services/`
	- GlobalAppParametersService : Obtencion y mapeo a clase del archivo de configuracion de aplicacion (JSon)
		- (Obsoleto) ConnectionStringService : Obtener cadena de conexion del SGBD desde el archivo de configuraicon de aplicacion (Json).
		- (Obsoleto) DatabasePathService : Obtener la ruta de acceso al SGBD desde el archivo de configuracion de aplicacion. (Local/Intranet).

`Common/Installation/` (Mantenerlo para ejecutar demo, Evaluar si es viable en aplicación monopuesto e independizarlo de infrastructure, para intranet mejor despligue manual)
	- Testeo de implementacion de base de datos con sus tablas, relaciones y datos semilla.