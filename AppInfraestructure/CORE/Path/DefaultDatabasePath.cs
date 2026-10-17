using System;
using System.IO;

using App.Infraestructure.CORE.Interfaces.Path;

/// <summary>
/// Return 'Application Database path directory'
/// </summary>

// FIXED : read from app.config key 'databasename'.
// FIXED : Modify AppXmlReader, implementation param is keyToSearch
// TODO : remove the enum
// UPGRADE : create StreamReader -> constructor with filepath, and return searched string.

namespace App.Infraestructure.CORE
{
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
