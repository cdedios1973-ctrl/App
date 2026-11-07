# ADR-005: Estrategia de Despliegue de la Base de Datos Maestra

## Estado
Aceptado

## Contexto
El sistema ya contaba con una validación para verificar la existencia de la base de datos antes de intentar el despliegue inicial. 
Sin embargo, si la base de datos existía físicamente pero la aplicación no lograba abrir una conexión con ella, 
el sistema fallaba de manera genérica. Esto ocultaba problemas críticos del entorno (como falta de permisos, bloqueos de red o corrupción de la instancia) 
y dificultaba el diagnóstico rápido en producción.

Actualmente dispongo de una carpeta `Installation` dentro del proyecto de infraestructura encargada de testear y desplegar el esquema físico.
Evaluando si la aplicación operará como una solución monopuesto (cliente único) con autodespligue, y, 
despliegue controlado (DBA) en un entorno de intranet corporativa.

## Decisión
Evolucionar la lógica de arranque de la aplicación hacia un enfoque de verificación condicional dual, clasificando el estado del sistema bajo los siguientes criterios:
1. Si NO existe físicamente: Se asume un entorno nuevo y se procede al despliegue automático de las estructuras y datos iniciales mediante scripts SQL.
2. Si SÍ existe físicamente pero NO abre conexión: Se identifica inmediatamente como un incidente/fallo de infraestructura. La aplicación detendrá su arranque, 
	bloqueará el despliegue e inyectará un log crítico detallando que la base de datos está inaccesible.
3. Si SÍ existe y SÍ conecta: Se omite el despliegue de infraestructura y la aplicación inicia normalmente.

# Negativas / Riesgos (A tener en cuenta)
Permisos elevados: Para que la aplicación pueda comprobar si la base de datos "existe físicamente" cuando no puede conectar a ella directamente, 
la cadena de conexión de diagnóstico podría requerir privilegios de administración (ej. acceso al catálogo del motor de base de datos como sys.databases o similar).

SI (La Base de Datos NO existe físicamente):
    -> Ejecutar scripts SQL de despliegue inicial.

SINO (La Base de Datos SÍ existe):
    INTENTAR abrir conexión:
        ÉXITO -> Continuar arranque normal (Saltar despliegue).
        FALLO -> Lanzar Excepción Crítica: "¡ALERTA! La Base de Datos existe pero no responde. Revisar credenciales, red o estado del motor."

        Redacción: Diseñado por el autor y optimizado con asistencia de IA para formato ADR. 
