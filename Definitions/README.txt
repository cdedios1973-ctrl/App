// TODO / NOTA DE ARQUITECTURA (SOLID - ISP / DIP):
// Los DTO deben tener las excepciones en constructor para avisar antes a la vista de los errores (solid?)
// Para futuras refactorizaciones, se puede eliminar los POCO/DTO de escritura intermedios.
// En su lugar, se puede definir interfaces (ej. IUsuarioRegistroData) que las propias 
// entidades de negocio o commands implementen directamente. 
// Esto reducirá el código repetitivo (boilerplate) y desacoplará el DAO de clases concretas.
