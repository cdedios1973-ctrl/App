# AppDefinitions

Este proyecto contiene los **elementos comunes de la aplicación**. 
*(Nota: De momento, los elementos específicos se mantienen en cada proyecto individual para acotar correctamente las referencias `using`).*

---

## 🛠️ Directriz de Desarrollo (Fail-Fast)
* **Regla para los DTO:** Los constructores deben incluir el manejo de excepciones (**Principio Fail-Fast**). 
* El objetivo es detectar y avisar a la vista de cualquier error lo antes posible.

---

## 📂 Estructura del Proyecto

A continuación se detalla el propósito de cada carpeta en esta refactorización:

* **`Deprecated/`**  
  Código previo, comentado y ya refactorizado. *Ideal para revisar el "antes y después" de la solución.*
* **`DTO/`**  
  Modelos de Objetos de Transferencia de Datos (Data Transfer Objects).
* **`Entity/`**  
  Modelos POCO (Plain Old CLR Objects) que representan las entidades base.
* **`Entity/Auth/`**  
  Modelos POCO específicos con los datos básicos del usuario activo en la aplicación.
* **`Enums/`**  
  Enumerados globales utilizados a lo largo de la aplicación.
* **`Events/`**  
  Manejadores internos de la aplicación. Se pueden implementar en el futuro para lanzar mensajes o notificaciones hacia la vista.
* **`Predicates/`**  
  Predicados y delegados personalizados para las búsquedas.
* **`Interfaces/`**  
  Contratos e interfaces generales que definen el comportamiento de la aplicación.
