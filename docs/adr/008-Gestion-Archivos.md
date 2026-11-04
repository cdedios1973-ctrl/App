# ADR-0008: Centralización de Infraestructura y Gestión de Archivos mediante DI

## Estado
Aceptado

## Contexto
La aplicación requiere interactuar frecuentemente con el sistema de archivos del disco duro (`System.IO`) para leer configuraciones (`appsettings.json`) y registrar trazas de eventos (Logs). 
Hacer llamadas directas a métodos estáticos de archivos o instanciar repetidamente lectores de disco mediante `new()` dentro de los servicios de negocio genera los siguientes problemas:
1. Acoplamiento duro al disco físico y a rutas de entornos locales.
2. Degradación del rendimiento debido a accesos reiterados de lectura y escritura en almacenamiento secundario (I/O Bottleneck).
3. Riesgo de bloqueo de descriptores de archivos en el sistema operativo si no se gestionan correctamente los flujos de datos.

## Decisión
Hemos decidido desacoplar por completo la infraestructura de persistencia implementando el patrón de Inyección de Dependencias (DI) bajo los siguientes criterios técnicos:
1. **Encapsulamiento SOLID:** Toda lógica de acceso a disco (`System.IO`) se aislará detrás de contratos e interfaces claras (ej. `IServicioArchivos`).
2. **Uso Defensivo de Recursos:** Es obligatorio el uso exclusivo del bloque sintáctico `using` (traducción nativa de `try/finally`) en operaciones como `File.Create` para garantizar la liberación inmediata de flujos de datos en el hardware.
3. **Frontera de Instanciación en el Main:** El método `Main()` asume la responsabilidad única de ejecutar sentencias `new()` y ensamblar el sistema.
4. **Ciclo de Vida Singleton para Configuraciones:** Los parámetros ligeros del archivo JSON de configuración se mapearán en memoria RAM mediante clases de dominio una sola vez al arrancar, inyectándose como servicios permanentes (`Singleton`) para optimizar el rendimiento.

## Consecuencias
### Positivas:
* **Hiperrendimiento:** Los accesos de lectura pasan de una escala de milisegundos en disco a nanosegundos directos en la memoria RAM.
* **Mantenibilidad:** Alta facilidad para migrar el sistema en el futuro hacia almacenamiento en la nube (AWS/Azure) cambiando una única línea de código en el archivo `Main()`.
* **Robustez:** Control absoluto de excepciones y estado de archivos sin fugas de memoria o bloqueos accidentales de recursos en el sistema operativo.

### Negativas / Costes:
* Incremento inicial en el volumen de código base debido a la creación explícita de abstracciones, interfaces y cableado de contenedores de servicios en el inicio de la aplicación.

## Enlaces Relacionados
* Ver guía completa de implementación en: `docs/research/01_JsonConfig.md`



        Redacción: Diseñado por el autor y optimizado con asistencia de IA para formato ADR. 
