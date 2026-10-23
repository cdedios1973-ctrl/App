using System;
using System.IO;
using System.Collections.Generic;

using App.Definitions.DTO;
using TaskHub.Infrastructure.Paths;

// ============================================================================
// REFACTORING LOG & ROADMAP
// ============================================================================
// DONE : Quitar el enumerado, solo una base de datos a la vez.
// DONE : Crear una clase propia para añadir (dto PathInfo): NombreDeArchivo, Ruta, TipoDeArchivo.
// DONE : Unir los servicios de ruta a uno -> para encontrar la base de datos y la config.
// DONE : Tener en cuenta directorios en Linux para soporte multiplataforma.
// DONE : Quitar el enumerado, solo una base de datos a la vez.
// DONE : Crear una clase propia para añadir (dto PathInfo) : NombreDeArchivo, Ruta, TipoDeArchivo
//
// DISCARD: Podria devolver un Dictionary<string, string> con las rutas necesarias para el uso de la aplicacion
// DISCARD: create StreamReader -> constructor with filepath, and return searched string.
//
// TODO : Al igual que el servicio de lectura de XML este proceso se tiene que lanzar al inicio de la aplicacion, no hay rutas dinamicas al vuelo, solo se lee una vez
// TODO : Crear un servicio de verificacion de archivos. "PathValidator" (Pospuesto).
// ============================================================================

namespace TaskHub.Infrastructure.Services
{
    /// <summary>
    /// Resuelve y expone las rutas globales, nombres y tipos de archivos necesarios 
    /// para el correcto funcionamiento de la aplicación.
    /// </summary>
    internal sealed class AppPathResolver : IPathProvider
    {
        private readonly string _baseDirectory;
        private readonly string _exePath;
        private readonly string _assemblyName;
        private readonly string _dbFolder;
        private readonly string _configFolder;

        #region Constructors

        internal AppPathResolver() 
        {
            this._exePath       = Environment.ProcessPath ?? string.Empty;
            if(string.IsNullOrEmpty(this._exePath))
                throw new FileNotFoundException(nameof(this._exePath));

            this._baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this._assemblyName  = Path.GetFileName(this._exePath);
            this._dbFolder      = Path.Combine(this._baseDirectory, "database");
            this._configFolder  = Path.Combine(this._baseDirectory, "config");
        }

        #endregion

        #region Interface Implementation

        public List<PathInfo> GetAppGlobalPaths()
        {
            var paths = new List<PathInfo>();

            paths.Add(new PathInfo("app", this._assemblyName, this._exePath, "Executable"));
            paths.Add(new PathInfo("db", "taskhub.db", this._dbFolder, "Database"));
            paths.Add(new PathInfo("cfg", "app.config", this._configFolder, "Configuration"));

            return paths;
        }

        #endregion

    }
}
