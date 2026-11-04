# AppDefinitions

Este proyecto contiene los **elementos comunes de la aplicación**. 
*(Nota: De momento, los elementos específicos se mantienen en cada proyecto individual para acotar correctamente las referencias `using`).*

---

## 📂 Estructura del Proyecto

A continuación se detalla el propósito de cada carpeta en esta refactorización:

* **`Deprecated/`**  
  Código previo, comentado y ya refactorizado. *Revisar el "antes y después" de la solución.*
* **`Enums/`**  (Deprecated) 
  Enumerados globales utilizados a lo largo de la aplicación.
* **`DTO/`**  
  Modelos de Objetos de Transferencia de Datos (Data Transfer Objects).
* **`DTO/Queries/`**  
  Modelos de Objetos de Transferencia de Datos (Data Transfer Objects) para consultas.
* **`Entity/Models/`**  
  Modelos POCO (Plain Old CLR Objects) que representan las entidades base.
* **`Entity/Auth/`**  
  Modelo específico con los datos básicos del usuario activo en la aplicación.
* **`Events/`**  
  Manejadores internos de la aplicación. Se pueden implementar en el futuro para lanzar mensajes o notificaciones hacia la vista.
* **`Predicates/`** 
  Predicados y delegados personalizados para las búsquedas.
* **`Interfaces/`**  
  Contratos e interfaces generales que definen el comportamiento de la aplicación.
