# ADR-001: Sistema Gestor de Bases de Datos (SGBD)

## Estado
Aceptado

## Contexto
La aplicación inicial incluía soporte para múltiples motores de bases de datos. Sin embargo, 
esto generaba ramificaciones de código complejas debido a que cada motor de base de datos posee comportamientos, 
dialectos SQL y optimizaciones específicas.

## Decisión
Dado el alcance actual de la aplicación, se decide limitar el soporte a un único motor de base de datos. 
Se elige **SQLite** como el motor de arranque (*starter*) del proyecto, 
interactuando con él mediante comandos nativos a través de ADO.Net para maximizar el rendimiento y el control.

## Consecuencias
* **Simplificación del código:** Se eliminan múltiples espacios de nombres, abstracciones innecesarias y dependencias a librerías de terceros.
* **Reducción de complejidad:** Desaparece la necesidad de mantener compatibilidad entre diferentes SGBD.
* **Control total:** Permite exprimir las características y comandos específicos de SQLite de forma nativa.
* **Compromiso (Trade-off):** La aplicación queda fuertemente acoplada a SQLite	; migrar a otro SGBD en el futuro requerirá una refactorización de la capa de acceso a datos.
