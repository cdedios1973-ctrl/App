using System;
using System.IO;

using App.Definitions.Enums;

using App.Infraestructure.CORE.Interfaces.Path;

namespace App.Infraestructure.CORE
{
    /// <summary>
    /// Return 'Application path directory'
    /// </summary>

    // TODO : Create service to determine "extension" <- 

    internal sealed class DefaultDatabasePath_Old : IAppPathProvider
    {
        private const string LOCAL_DATABASE_NAME = "AppDemoDatabase.";
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _extension;

        #region Constructors

        internal DefaultDatabasePath_Old(DataBaseType dbType = DataBaseType.SqLite)
        { 
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
                throw new ArgumentNullException("Extension must be filled.", nameof(dbType));

            if (dbType == DataBaseType.SqlServer)
                this._extension = "";

            this._extension = (dbType == DataBaseType.SqLite ? "" : "");
        }

        #endregion

        #region Interface Implementation

        public string GetPath()
        {
            string fullpath = Path.Combine(_baseDirectory, LOCAL_DATABASE_NAME + this._extension);

            if (!File.Exists(fullpath))
            {
                throw new FileNotFoundException(fullpath);
            }
            return fullpath;
        }

        #endregion

    }
}
