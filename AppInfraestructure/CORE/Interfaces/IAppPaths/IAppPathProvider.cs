using System;

namespace App.Infraestructure.CORE.Interfaces.Path
{
    /// <summary>
    /// Esta interfaz define las acciones básicas obtener la ruta de acceso.
    /// </summary>
    internal interface IAppPathProvider
    {
        /// <summary>
        /// Devuelve la ruta de Acceso.
        /// </summary>
        /// <returns>Devuelve true si el registro se insertó correctamente; de lo contrario, false.</returns>
        public string GetPath();
    }

}
