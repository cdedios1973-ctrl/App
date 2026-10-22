using System;

using App.Definitions.DTO;

//using AppDefinitions.Deprecated.DataFilters;
// TODO(NO) : DataFilter -> Pendiente de definir y testear. añadirle interfaz
// TODO(NO) : Creo que si le pongo como segundo parametro isSearch a true, puedo usarlo como plantilla del where

namespace App.Infrastructure.Persistence
{
    /// <summary>
    /// Esta interfaz define la accion básica para leer datos de la tabla Module en la base de datos.
    /// </summary>
    public interface IModuleReadRepository
    {
        /// <summary>
        /// Lee registro/s de Modulos/s. Requiere nivel de permiso 1 o superior.
        /// </summary>
        /// <param name="module">El DTO de Module.</param>
        /// <param name="filter">El filtro de la consulta a la base de datos.</param>
        /// <returns>Devuelve una lista de Modulos (dto).</returns>
        List<ModuleDTO> Select(ModuleDTO module); //, DataFilter filter);

    }

}