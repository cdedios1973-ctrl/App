# AppInfrastructure

El objetivo de este ensamblado es contener una serie de funcionalidades basicas para consumir tanto en Infraestructura, Logica.

---

## 📂 Estructura del Proyecto

A continuación se detalla el propósito de cada carpeta en esta refactorización:

* **`Deprecated/`**  
  Código previo, comentado y ya refactorizado. *Ideal para revisar el "antes y después" de la solución.*
* **`Install - Refactoring/`**  
  Código previo, en estado actual de refactorización
* **`Common/`**  
  Coleccion de herramientas básicas de la aplicación.
* **`Installation/`**  
  Autoinstalacion de bases de datos para entorno demo, leer ADR-005.
* **`Docs`**  
  Documentacion ADR.
* **`Persistance/`**  
  Definicion de Contratos e implementacion de los accesos a base de datos R/W.
* **`Services/`**  
  Definicion de los servicios que expone este ensamblado (WIP)

*** Detalles de estructuras en las que se esta trabajando actualmente:
----------------------------------------------------------------------
`Common/`
	- 'Interfaces/' : Definiciones de las interfaces de uso interno.
	- 'XMLReader/'  : XML para leer documentos internos de configuracion.
`Services/`
	- GlobalAppParametersService : Obtencion y mapeo a clase del archivo de configuracion de aplicacion (JSon)
		- (Obsoleto) ConnectionStringService : Obtener cadena de conexion del SGBD desde el archivo de configuraicon de aplicacion (Json).
		- (Obsoleto) DatabasePathService : Obtener la ruta de acceso al SGBD desde el archivo de configuracion de aplicacion. (Local/Intranet).

`Installation/` (Mantenerlo para ejecutar demo, Evaluar si es viable en aplicación monopuesto e independizarlo de infrastructure, para intranet mejor despligue manual)
	- Testeo de implementacion de base de datos con sus tablas, relaciones y datos semilla.