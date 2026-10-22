using System;
using System.IO;

using TaskHub.Infrastructure.Path;

// TODO : Unir los servicios de ruta a uno -> para encontrar la base de datos y para encontrar el archivo de configuracion
// TODO : Es mas, podria devolver un Dictionary<string, string> con las rutas necesarias para el uso de la aplicacion
// TODO : Tener en cuenta como se estructuran los directorio en Linux, por si hay que implementar una segunda interfaz de SO.
// TODO : Ej : (app root/database) (app root/Config)

namespace TaskHub.Infrastructure.Services
{
    /// <summary>
    /// Return 'base' directory of the Application configuration file
    /// </summary>
    internal sealed class DefaultAppConfigPath : IAppPathProvider
    {
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _assemblyName = AppDomain.CurrentDomain.FriendlyName;
        private readonly string _extension;

        #region Constructors

        internal DefaultAppConfigPath(string extension = ".config")
        {
            if (extension == "")
            {
                throw new ArgumentNullException("Extension cannot be empty or null" + nameof(extension));
            }
            this._extension = extension;
        }

        #endregion

        #region Interface Implementation

        public string GetPath() // TODO : cambiar la implementacion del interface para que admita lo que voy a buscar. (base de datos o archivo de configuracion)
        {
            string fullpath = Path.Combine(_baseDirectory, $"{_assemblyName}" + this._extension);

            if (!File.Exists(fullpath))
            {
                throw new FileNotFoundException(fullpath);
            }
            return fullpath;
        }

        #endregion

    }
}
