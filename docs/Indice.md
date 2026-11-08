# Índice de Registros de Decisiones de Arquitectura (ADR)

Este documento contiene el registro cronológico de las decisiones de arquitectura tomadas en el proyecto. Su objetivo es mantener el contexto histórico de por qué se eligieron o descartaron ciertas tecnologías, patrones o estructuras.

## Estados de un ADR
*   ** Propuesto (Proposed):** La decisión se está debatiendo.
*   ** Aceptado (Accepted):** La decisión ha sido aprobada y se está aplicando.
*   ** Superado (Superseded):** Una decisión más reciente ha reemplazado a esta.
*   ** Rechazado (Rejected):** La propuesta fue evaluada pero no se aprobó.

---

## Registro de Decisiones

| ID	| Título de la Decisión															| Estado	| Fecha		 | Reemplaza a |
| ------------------------------------------------------------------------------------------------------------------------------
| 0001	| Sistema Gestor de Bases de Datos												| Aceptado	| 2026-10-00 |  		|
| 0002	| Transacciones Maestro Detalle													| Superado	| 2026-10-00 |  		|
| 0003	| Autodespliegue de Base de Datos												| Aceptado	| 2026-10-00 |  		|
| 0004	| Centralización del Lector de Configuración Exclusivo							| Superado	| 2026-10-00 |			|
| 0005	| Estrategia de Despliegue de la Base de Datos Maestra							| Aceptado	| 2026-10-00 |			|
| 0006	| Diseño del Módulo de Autenticación de Usuarios								| Aceptado	| 2026-10-26 |			|
| 0007	| VeriFactu, Postergación de Cumplimiento										| Propuesto	| 2026-10-00 |  		|
| 0008	| Gestión de Archivos mediante DI												| Aceptado	| 2026-10-00 | ADR-0004 |
| 0009	| Flujo de Facturación Concurrente												| Estudio	| 2026-11-00 |			|
| 0010	| Segregacion de Repositorios de Escritura y Control de Transacciones Nativo	| Aceptado	| 2026-00-00 | ADR-0002 |
| 0011	| Copias de Seguridad de Base de Datos SQLite									| Aceptado	| 2026-11-08 |			|

---

## Notas de mantenimiento
*   Cada nuevo ADR debe seguir la plantilla oficial establecida en `/docs/adr/0000-plantilla.md`.
*   Al crear un nuevo ADR, incrementa el ID en uno (`0004`, `0005`, etc.) y actualiza inmediatamente esta tabla.
*   Si un ADR reemplaza a otro anterior, asegúrate de cambiar el estado del ADR antiguo a **Superado** y enlazarlo en la columna correspondiente.
