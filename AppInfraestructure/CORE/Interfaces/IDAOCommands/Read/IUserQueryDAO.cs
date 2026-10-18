using System;

using App.View.Models;
using App.Definitions.Filters;

// TODO : DataFilter -> Pendiente de definir y testear. añadirle interfaz
// TODO : Creo que si como segundo parametro le pongo isSearch a true, puedo usarlo como plantilla del where

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
        List<UserDTO> Select(UserDTO user, DataFilter filter);

    }

}