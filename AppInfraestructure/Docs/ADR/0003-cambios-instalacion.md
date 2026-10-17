# 1. Cambio en el autodespliegue de base de datos maestra para aplicacion.

## Estado
Aceptado

## Contexto
El despliegue incial contenía la creación tanto de bases de datos, tablas y registros inciales
en Clases de código, y comprobaba la existencia fisica del archivo antes de ejecutar la instalación.

## Decisión
Cambiar a Scripts SQL.

## Consecuencias
* Mejor uso de las herramientas.
* Claridad de código.