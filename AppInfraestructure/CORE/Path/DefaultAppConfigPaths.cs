using System;
using System.IO;

using App.Definitions.DTO;
using TaskHub.Infrastructure.Paths;

// DISCARD: Es mas, podria devolver un Dictionary<string, string> con las rutas necesarias para el uso de la aplicacion
// DISCARD: create StreamReader -> constructor with filepath, and return searched string.
// DONE : read from app.config key 'databasename'.
// DONE : Modificar AppXmlReader, implementar parammetro keyToSearch
// DONE : Quitar el enumerado, solo una base de datos a la vez.
// DONE : Crear una clase propia para añadir (dto PathInfo) : NombreDeArchivo, Ruta, TipoDeArchivo
// DONE :
    // la lista va a devolver:
    // nombre de ejecutable y su ruta - 
    // nombre de la base datos (fija) y su carpeta (miexe.exe/database)
    // nombre de archivo app.config (fijo) y su carpeta (miexe.exe/config)
    // Ej : (app root/database) (app root/Config)

// DONE : Unir los servicios de ruta a uno -> para encontrar la base de datos y para encontrar el archivo de configuracion

// TODO : Crear un servicio de verificacion de archivos. "PathValidator"
// TODO : Tener en cuenta como se estructuran los directorio en Linux, por si hay que implementar una segunda interfaz de SO.

namespace TaskHub.Infrastructure.Services
{
    /// <summary>
    /// Clase que devuelve una lista de elementos, que contiene las rutas, nombres y tipos de archivos necesarios para el correcto funcionamiento.
    /// </summary>
    internal sealed class DefaultAppConfigPaths : IPathProvider
    {
        private readonly string _baseDirectory  = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _assemblyName   = AppDomain.CurrentDomain.FriendlyName;
        private readonly string _exePath        = null;
        private readonly string _dbFolder       = null;
        private readonly string _configFolder   = null;

        #region Constructors

        internal DefaultAppConfigPaths() 
        {
            this._exePath       = Path.Combine(this._baseDirectory, this._assemblyName);
            this._dbFolder      = Path.Combine(this._baseDirectory, "database");
            this._configFolder  = Path.Combine(this._baseDirectory, "config");
        }

        #endregion

        #region Interface Implementation

        public List<PathInfo> GetAppGlobalPaths()
        {
            var paths = new List<PathInfo>();

            paths.Add(new PathInfo(this._assemblyName, this._exePath, "Executable"));
            paths.Add(new PathInfo("taskhub.db", this._dbFolder, "Database"));
            paths.Add(new PathInfo("app.config", this._configFolder, "Configuration"));

            return paths;
        }

        #endregion

    }
}
