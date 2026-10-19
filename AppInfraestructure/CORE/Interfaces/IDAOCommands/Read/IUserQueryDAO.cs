using System;

using App.Definitions.DTO;

// TODO(NO) : DataFilter -> Pendiente de definir y testear. añadirle interfaz
// TODO(NO) : Creo que si como segundo parametro le pongo isSearch a true, puedo usarlo como plantilla del where
// TODO     : Usar "miClaseDTOFilter" desde la vista, creo que con este concepto puedo usar consultas LEFT/RIGHT/INNER basicos, necesitare identificador.
// TODO     : (El dto de vista ya contiene la combinacion. dejarlo exclusivo para su uso original, no tocar.)

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    /// <summary>
    /// Esta interfaz define la accion básica para leer datos de la tabla User en la base de datos.
    /// </summary>
    public interface IUserQueryDAO
    {
        /// <summary>
        /// Lee registro de usuario. Requiere nivel de permiso 1 o superior.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <param name="filter">El filtro de la consulta a la base de datos.</param>
        /// <returns>Devuelve una lista de usuarios (dto).</returns>
        List<UserDTO> UserQueryDAO(UserDTO user);//, DataFilter filter);

    }

}