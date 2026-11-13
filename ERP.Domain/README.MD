# ERP.Domain (AppDefinitions Refactorización)

Este proyecto contiene los **elementos comunes y las reglas de negocio base** de la aplicación.

---

## 📂 Estructura del Proyecto

Propósito de cada carpeta en esta nueva estructura:

* **`Deprecated/`**  
  Código previo y comentado. Sirve de referencia para revisar el "antes y después" de la refactorización.
* **`DTO/`**  
  Objetos de Transferencia de Datos (Data Transfer Objects) para el intercambio de información.
  * **`Queries/`** DTOs específicos estructurados para consultas y reportes.
* **`Entity/`**  
  Modelos base del dominio.
  * **`Models/`** Clases POCO que representan las entidades principales.
  * **`Auth/`** Datos básicos y estado del usuario activo.
  * **`AppConfig/`** Configuración global de la aplicación.
* **`Events/`**  
  Manejadores de eventos internos para la comunicación desacoplada del sistema.
* **`Predicates/`**  
  Predicados y delegados personalizados para filtros y búsquedas avanzadas.
* **`Interfaces/`**  
  Contratos globales que definen el comportamiento de la aplicación (`ConnectionString`, `Controller`, `Services`, `WinForms`).

  