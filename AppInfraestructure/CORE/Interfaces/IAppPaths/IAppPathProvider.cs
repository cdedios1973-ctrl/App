using System;

namespace TaskHub.Infrastructure.Path
{
    /// <summary>
    /// Esta interfaz define las acciones básicas obtener la ruta de acceso.
    /// </summary>
    internal interface IAppPathProvider
    {
        /// <summary>
        /// Devuelve la ruta de Acceso.
        /// </summary>
        /// <returns>Devuelve la ruta de acceso, vacio si no existe</returns>
        public string GetPath();
    
    }

}
