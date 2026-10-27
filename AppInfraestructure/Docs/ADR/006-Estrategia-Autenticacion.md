# ADR 006: Gestión de Identidad Interna y Postergación de Cumplimiento VeriFactu

## Estado
Propuesto / Aceptado

## Contexto
La aplicación de escritorio requiere identificar qué usuario está realizando operaciones dentro del sistema para mantener un registro de auditoría interna 
y gestionar permisos. Adicionalmente, se prevé que en el futuro el sistema deba cumplir con la normativa legal española VeriFactu 
(Reglamento de Sistemas de Facturación de la AEAT). 

Dado que el módulo de VeriFactu requiere un desarrollo complejo de encadenamiento criptográfico, firmas digitales y envíos XML a la administración pública 
que está fuera del alcance de la fase actual del proyecto, necesito una solución intermedia para la identidad sin bloquear el desarrollo.

## Alternativas Descartadas
* **Sistema de Login "Antiguo en pruebas" propio:** Se evaluó mantener o migrar un sistema de autenticación heredado (legacy) basado en configuraciones complejas hechas a medida.
	**Se descarta por completo** debido a su alto coste de mantenimiento, falta de estándares modernos de seguridad y la complejidad innecesaria que añadía al código base de la aplicación de escritorio en esta etapa.

## Decisión
1. **Implementar una Solución de Sesión Interna:** Utilizare el mecanismo estándar de .NET (`Thread.CurrentPrincipal`) alimentado por un servicio de 
	autenticación propio (`CustomAuthType`) para propagar la identidad del usuario en el hilo de ejecución de la app.
2. **Auditoría Doméstica:** Los registros de auditoría actuales reflejarán únicamente el usuario interno que ha interactuación con el sistema, 
	con fines puramente operativos y de depuración informática.
3. **Postergación de VeriFactu:** Se decide formalmente **no implementar** ninguna lógica de VeriFactu en esta etapa. 
	El cumplimiento normativo se tratará como un hito independiente en el futuro.

## Consecuencias
* **Positivas:** Desarrollo más rápido, código desacoplado de normativas legales cambiantes en la fase inicial, y arquitectura interna limpia gracias al uso de `ClaimsPrincipal`.
* **Negativas:** El registro de auditoría actual NO es válido ante una inspección fiscal de la AEAT.
* **Próximos pasos (Mitigación):** Cuando el proyecto alcance la fase de cumplimiento legal, será obligatorio subcontratar o pedir asesoría a profesionales
	expertos en VeriFactu/AEAT para diseñar el registro informático de facturación (RIF), el encadenamiento de facturas y la firma digital. En ese momento, 
	se evaluará también si se migra el Login interno a la autorización de Microsoft (MSAL) para elevar la seguridad.