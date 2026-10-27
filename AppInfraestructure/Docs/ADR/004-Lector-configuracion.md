# ADR-004: Centralización del Lector de Configuración Exclusivo

## Estado
Aceptado

## Contexto
Se requiere unificar los parámetros de configuración que se encontraban dispersos 
(anteriormente gestionados por `ConnectionStringService` y `DatabasePathService`) en un 
único punto centralizado (`GlobalAppParametersService`) utilizando un archivo JSON plano como origen.

## Decisión
Implementar la lectura del archivo físico en disco y la posterior deserialización JSON dentro 
de la misma clase `GlobalAppParametersService` ubicada en el ensamblado de infraestructura, 
aceptando de forma consciente este acoplamiento.

## Justificación
El componente es de uso exclusivo y no se prevé su extensión para soportar otros formatos (como XML o YAML) 
durante el ciclo de vida de la aplicación. Separar el acceso a disco de la deserialización añadiría una capa de abstracción 
y sobreingeniería innecesaria para el alcance actual del proyecto.

## Consecuencias
* **Alta cohesión:** El código de configuración queda concentrado y es fácil de mantener en un único archivo.
* **Trade-off (Principio SRP):** Se viola estrictamente el Principio de Responsabilidad Única (SRP) 
* al combinar E/S de archivos con análisis sintáctico (parsing) JSON en la misma entidad. 
* No obstante, el impacto es mínimo debido a la baja probabilidad de cambio de formato.
