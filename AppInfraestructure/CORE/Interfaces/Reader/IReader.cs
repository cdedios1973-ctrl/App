using System;

namespace App.Infraestructure.CORE.Interfaces.Reader
{
    public interface IAppXmlReader
    {
        public string GetValueByKey(string keyToSearch);
    }
}
