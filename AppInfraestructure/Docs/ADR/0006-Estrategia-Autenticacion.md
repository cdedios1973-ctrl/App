# ADR 006: Estrategia de Autenticación y Niveles de Permiso R/W

## Estado
**APROBADO / EN PROCESO** ⚙️

## Contexto
Inicialmente se planteó usar una clase temporal (`UserAccess`) para salir del paso. Sin embargo, para dar consistencia al sistema, 
se ha descartado ese enfoque temporal en favor de un sistema de login básico real que valida las credenciales directamente contra una tabla dedicada (`userlogin`).

Con esta base ya resuelta, queda pendiente diseñar cómo se escalará la seguridad y cómo se gestionarán los permisos de lectura y escritura 
en la base de datos desde la capa de Lógica.

## Decisiones Implementadas
1. **Eliminación de temporalidades:** Se descarta por completo el uso de la clase `UserAccess`.
2. **Persistencia del Login:** Implementación de un flujo de acceso real que consulta y valida contra la tabla `userlogin`.

## Propuesta para Siguientes Fases (A Futuro)
A partir del login básico actual, se plantean las siguientes mejoras arquitectónicas:

1. **Segregación de Bases de Datos:** Evaluar la separación física del login para aislar la seguridad:
   * **BD de Login:** Exclusiva para credenciales (`user`, `pass`, `userId`).
   * **BD Principal:** Contenedora del resto de las tablas de negocio.
2. **Control de Accesos por Niveles (R/W) en la Capa Lógica:**
   * **Consultas de Lectura (R):** Requerirán nivel de permiso 1 o superior.
   * **Consultas de Escritura (W):** Requerirán nivel de permiso 2 o superior.
3. **Evolución Técnica:** Estudiar las soluciones oficiales de Autenticación de Microsoft para sustitución a largo plazo.

## Consecuencias
* **Positivas:** El proyecto cuenta ya con un flujo de acceso real y conectado a base de datos.
* **Negativas:** 
  * **Tiempo de desarrollo:** El tiempo de refactorización del proyecto se prolonga significativamente al tener que reestructurar las capas.
  * **Ciclos de compilación:** Obliga a tener que volver a compilar todos los ensamblados de la solución con cada cambio estructural.
  * **Riesgo de escalabilidad:** Si no se gestiona bien la separación de las bases de datos y los permisos desde el principio, se pueden generar problemas de escalabilidad y rendimiento a largo plazo.