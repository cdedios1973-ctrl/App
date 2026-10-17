using System;

namespace App.Infraestructure.CORE.Interfaces.Reader
{
    public interface IConnectionStringService
    {
        string GetConnectionString();
    }

    public interface IAppXmlReader
    {
        public string GetValueByKey(string keyToSearch);
    }
}
