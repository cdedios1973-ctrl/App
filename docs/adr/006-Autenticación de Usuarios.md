# ADR 006: Diseño del Módulo de Autenticación de Usuarios

## Estado
Aceptado

## Contexto
La aplicación de escritorio requiere identificar qué usuario está realizando operaciones dentro del sistema para mantener un registro de auditoría interna 
y gestionar permisos.

- Necesito almacenar las credenciales de acceso de los usuarios.
- Actualmente la aplicación está enfocada a un entorno de Intranet.
- No dispongo de tiempo/recursos para implementar el sistema complejo de Tokens/Roles de Microsoft.


## Alternativas Descartadas
* **Sistema de Login "Antiguo en pruebas" propio:** Se evaluó mantener o migrar un sistema de autenticación heredado (legacy) basado en configuraciones complejas hechas a medida.
	**Se descarta por completo** debido a su alto coste de mantenimiento, falta de estándares modernos de seguridad y la complejidad innecesaria que añadía al código base de la aplicación de escritorio en esta etapa.

## Decisión
1. **Implementar una Solución de Sesión Interna:** Utilizare el mecanismo estándar de .NET (`Thread.CurrentPrincipal`) alimentado por un servicio de 
	autenticación propio (`CustomAuthType`) para propagar la identidad del usuario en el hilo de ejecución de la app.
2. **Auditoría Doméstica:** Los registros de auditoría actuales reflejarán únicamente el usuario interno que ha interactuación con el sistema, 
	con fines puramente operativos y de depuración informática.
3. ** Separar las credenciales en una tabla maestra `UserLogin` con relación 1:1 hacia `User`. 
	- Usar autenticación básica temporalmente.
	- Aplicar restricciones (constraints) en la base de datos para asegurar un único `UserId` por registro.

## Consecuencias
* **Negativas:** No contar de entrada con el mejor sistema de seguridad.
* **Deuda técnica** Requiere una migración futura a Microsoft Identity o JWT Tokens cuando el sistema escale.

 
        Redacción: Diseñado por el autor y optimizado con asistencia de IA para formato ADR. 