using System;

namespace App.Infrastructure.Reader
{
    public interface IAppXmlReader
    {
        public string GetValueByKey(string keyToSearch);
    }
}
