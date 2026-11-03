# Refactorización (WIP) de Recopilación de ejemplos de Software a Prototipo de ERP Local (.NET / WinForms)


Este repositorio contiene la refactorización arquitectónica de un sistema piloto local, rediseñado bajo principios de arquitectura limpia y desarrollo robusto para garantizar el hiperrendimiento en hardware modesto, la mantenibilidad a largo plazo y la compatibilidad nativa con .NET Native AOT.


## Documentación de Arquitectura


Para entender las decisiones de diseño, el flujo de datos y la gestión del hardware, consulta los siguientes documentos estratégicos:

*   **[Manual de Arquitectura](./docs/manual-arquitectura.md):** Fundamentos del manejo seguro de archivos (`System.IO`), el JIT y la Inyección de Dependencias (DI) sin penalización en memoria.
*   **[Registros de Decisión Arquitectónica (ADRs)](./docs/adr/):** El histórico de diseño que justifica las soluciones técnicas aplicadas:
    *   [ADR-001: SQLite y ADO.NET Nativo para control total](./docs/adr/adr-001.md)
    *   [ADR-002: Gestión de Transacciones Maestro-Detalle](./docs/adr/adr-002.md)
    *   [ADR-008: Centralización de Infraestructura mediante DI](./docs/adr/adr-008.md)
    *   [ADR-009: Flujo de Facturación Concurrente e Inmutabilidad](./docs/adr/adr-009.md)

## Pilares Técnicos del Proyecto y Objetivo a alcanzar (WIP)
*   **SOLID & Clean Architecture:** Separación absoluta de jurisdicciones. Capa de lógica puramente inmutable.
*   **Rendimiento en Red Local:** Activación de SQLite en Modo WAL (Write-Ahead Logging) para evitar bloqueos concurrentes.
*   **Zero-Reflection & Native AOT:** Código estático optimizado con Source Generators para compilar directamente a código máquina nativo.
