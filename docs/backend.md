
## 1. Lo que tiene que saber un desarrollador backend moderno

Si no se hace desde el primer momento, la aplicación nacerá rota y subcontratar a alguien será tirar el dinero:

	1. No confiar nunca en el cliente: El frontend (JSON, formularios, inputs) se puede manipular. Tu controlador debe validar y limpiar cada dato que entra [^1].
	2. Encriptar contraseñas: Guardar contraseñas usando algoritmos seguros (como Bcrypt o Argon2), nunca en texto plano.
	3. Proteger las rutas: Asegurar que tu código verifique el token moderno en cada petición antes de entregar datos privados.
	4. Prevenir el Top 10 de OWASP: Conocer los ataques básicos, como la Inyección SQL (evitada hoy en día gracias al uso de ORMs) o el Cross-Site Scripting (XSS).


## 2. Lo que se delega en herramientas y frameworks

Los frameworks modernos ya traen escudos integrados que se deben configurar bien:

	1. Usar librerías estándar de JWT.
	2. Usar un ORM (como Sequelize, Hibernate o Entity Framework) que evita inyecciones de código de forma automática.


## 3. ¿Cuándo se subcontrata a los expertos en ciberseguridad?

	1. Auditorías de código (Pentesting): Cuando la web ya está lista para salir al mercado, ellos intentan hackearla "amigablemente" para ver si dejaste alguna puerta abierta.
	2. Cumplimiento legal (Compliance): Si vas a manejar datos médicos o pasarelas de pago bancarias directas (normativas como PCI-DSS o RGPD), necesitas expertos que certifiquen que la infraestructura cumple la ley.


"Si construyes una casa, tú como backend eres el arquitecto y el albañil. Tienes que saber poner columnas firmes y cerrar bien las ventanas con llave para que no entre cualquiera.
El experto subcontratado es el inspector que viene al final a revisar si el terreno es seguro ante terremotos y si las cerraduras son de alta calidad."