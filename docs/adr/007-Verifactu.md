# ADR 006: Postergación de Cumplimiento VeriFactu

## Estado
Propuesto

## Contexto
Se prevé que en el futuro el sistema deba cumplir con la normativa legal española VeriFactu (Reglamento de Sistemas de Facturación de la AEAT). 
Dado que el módulo de VeriFactu requiere un desarrollo complejo de encadenamiento criptográfico, firmas digitales y envíos XML a la administración pública,
de momento queda fuera del alcance en la fase actual del proyecto.

## Decisión
1. **Postergación de VeriFactu:** decido formalmente **no implementar** ninguna lógica de VeriFactu en esta etapa. 
	El cumplimiento normativo se tratará como un hito independiente en el futuro.

## Consecuencias
* **Próximos pasos (Mitigación):** Cuando el proyecto alcance la fase de cumplimiento legal, será obligatorio pedir asesoría a profesionales
	expertos en VeriFactu/AEAT para diseñar el registro informático de facturación (RIF), el encadenamiento de facturas y la firma digital. 
	En ese momento, se evaluará también si se migra el Login interno a la autorización de Microsoft (MSAL) para elevar la seguridad.

        Redacción: Diseñado por el autor y optimizado con asistencia de IA para formato ADR. 