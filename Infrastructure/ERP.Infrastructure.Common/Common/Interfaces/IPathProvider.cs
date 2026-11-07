using System;
using ERP.Domain.Entity.AppConfig;

namespace ERP.Infrastructure.Paths;
/// <summary>
/// Esta interfaz define la accion básica de obtener las rutas de acceso necesarias para el correcto funcionamiento de la aplicacion.
/// </summary>
internal interface IPathProvider
{
    /// <summary>
    /// Devuelve todas las rutas de Acceso en appsettings.json, App, Ruta de Acceso a la base de datos.
    /// </summary>
    /// <returns>Devuelve las rutas de acceso a traves de la clase AppConfigEntity</returns>
    public AppConfigEntity GetAppGlobalPaths();

}
