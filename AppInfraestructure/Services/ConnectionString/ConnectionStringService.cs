using System;

using App.Definitions.Enums;
using App.Infraestructure.CORE;
using App.Infraestructure.CORE.Interfaces.Path;
using App.Infraestructure.CORE.Interfaces.Reader;
using App.Infraestructure.CORE.Reader;

namespace App.Infraestructure.Services
{
    internal sealed class ConnectionStringService : IConnectionStringService
    {
        private const string DATABASE_TYPE_ERROR  = "Must select Database type.";
        private readonly string _connection;

        internal ConnectionStringService(DataBaseType dbType)
        {
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
            {
                throw new ArgumentException(DATABASE_TYPE_ERROR, nameof(dbType));
            }

            // Fixed Path, application configuration files must be there.
            IAppPathProvider pathProvider = new DefaultAppConfigPath();

            // Fixed to xmlreader, if later add filestream, add to ConnectionStringService constructor
            IAppXmlReader xmlReader = new AppXmlReader(pathProvider, dbType);
            
            _connection = xmlReader.GetValueByKey();
        }

        public string GetConnectionString()
        {
            return this._connection;
        }
    }
}
