using System;
using System.IO;

using TaskHub.Infrastructure.Path;

// TODO : Unir los servicios de ruta a uno -> para encontrar la base de datos y para encontrar el archivo de configuracion
// TODO : Tener en cuenta como se estructuran los directorio en Linux, por si hay que implementar una segunda interfaz de SO.
// TODO : Ej : (app root/database) (app root/Config)

// FIXED : read from app.config key 'databasename'.
// FIXED : Modify AppXmlReader, implementation param is keyToSearch
// TODO : Quitar el enumerado, solo una base de datos a la vez.
// UPGRADE : create StreamReader -> constructor with filepath, and return searched string.



namespace TaskHub.Infrastructure.Services
{
    /// <summary>
    /// Return 'Application Database path directory'
    /// </summary>
    internal sealed class DefaultDatabasePath : IAppPathProvider
    {
        //private const string LOCAL_DATABASE = "DataBase/AppDemoDatabase.";
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _localDatabase;

        #region Constructors

        internal DefaultDatabasePath(string fileName) // <- Could change for interface
        { 
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("Filename can not be empty.", nameof(fileName));
        }

        #endregion

        #region Interface Implementation


        // TODO : cambiar la implementacion del interface para que admita lo que voy a buscar. (base de datos o archivo de configuracion)
        public string GetPath()
        {
            string fullpath = Path.Combine(_baseDirectory, this._localDatabase);

            if (!File.Exists(fullpath))
            {
                throw new FileNotFoundException(fullpath);
            }
            return fullpath;
        }

        #endregion

    }
}
