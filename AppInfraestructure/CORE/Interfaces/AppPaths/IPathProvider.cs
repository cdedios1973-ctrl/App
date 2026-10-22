using System;

using App.Definitions.DTO;

namespace TaskHub.Infrastructure.Paths
{
    /// <summary>
    /// Esta interfaz define la accion básica de obtener las rutas de acceso necesarias para el correcto funcionamiento de la aplicacion.
    /// </summary>
    internal interface IPathProvider
    {
        /// <summary>
        /// Devuelve todas las rutas de Acceso, App.config, Ruta de Acceso a la base de datos.
        /// </summary>
        /// <returns>Devuelve las rutas de acceso, vacio si no existen u error</returns>
        public List<PathInfo> GetAppGlobalPaths();
    
    }

}
