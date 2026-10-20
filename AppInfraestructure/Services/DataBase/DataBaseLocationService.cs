using App.Definitions.Enums;
using App.Infraestructure.CORE;
using App.Infraestructure.CORE.Interfaces.Path;
using App.Infraestructure.CORE.Interfaces.Reader;
using System;

/// <Summary>
/// DataBaseFolderService -> Return database full path and filename
/// </Summary>

namespace App.Infraestructure.Services
{
    internal sealed class DataBaseLocationService //: IDatabaseFolderService
    {
        private const string DATABASE_TYPE_ERROR  = "Must select Database type.";
        private const string LOCAL_DATABASE = "DataBase/AppDemoDatabase.";
        private readonly string _dbPath;

        internal DataBaseLocationService(DataBaseType dbType, IAppXmlReader xmlReader)
        {
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
                throw new ArgumentException(DATABASE_TYPE_ERROR, nameof(dbType));

            string tmp = LOCAL_DATABASE; // Convert enum to string but need to read from app.config key 'databasename' -> to avoid if/else

            // FIXED : read from app.config key 'databasename'. <- done, at constructor.
            // FIXED : Modify AppXmlReader, second param is keyToSearch <- done, at AppXmlReader
            // TODO : remove the enum 
            // TODO : create StreamReader -> constructor with filepath, and return searched string.


            // Fixed Path, application database files must be there.
            IAppPathProvider pathProvider = new DefaultDatabasePath(tmp);

            this._dbPath = pathProvider.GetPath();
        }

        public string GetDataBasePath()
        {
            return this._dbPath;
        }
    }
}
