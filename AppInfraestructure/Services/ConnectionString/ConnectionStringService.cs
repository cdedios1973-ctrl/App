using System;

//using App.Infraestructure.CORE;
//using App.Infraestructure.CORE.Interfaces.Path;
//using App.Infraestructure.CORE.Reader;
using App.Definitions.Enums;
using App.Infraestructure.CORE.Interfaces.Reader;

/// <summary>
/// Class ConnectionStringService : Return the connection string of the indicated database.
/// </summary>

namespace App.Infraestructure.Services
{
    internal sealed class ConnectionStringService : IConnectionStringService
    {
        private const string DATABASE_TYPE_ERROR    = "Must select Database type.";
        private const string DATABASE_SUFFIX        = "Connection";
        private readonly string _key;
        private readonly IAppXmlReader _xmlReader;

        #region Constructor

        internal ConnectionStringService(DataBaseType dbType, IAppXmlReader xmlReader)
        {
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
                throw new ArgumentException(DATABASE_TYPE_ERROR, nameof(dbType));

            this._xmlReader = xmlReader;
            this._key = dbType.ToString();
        }

        #endregion

        #region Interface Implementation

        public string GetConnectionString()
        {
            // Avoid use of new() -> need to expose in constructor.
            // Fixed Path, application configuration files must be there.
            //IAppPathProvider pathProvider = new DefaultAppConfigPath();
            //IAppXmlReader xmlReader = new AppXmlReader(pathProvider, this._key);
            //return xmlReader.GetValueByKey();

            return this._xmlReader.GetValueByKey(this._key + DATABASE_SUFFIX);
        }

        #endregion
    }
}
