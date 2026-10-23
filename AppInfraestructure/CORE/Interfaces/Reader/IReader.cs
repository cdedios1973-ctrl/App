using System;

namespace TaskHub.Infrastructure.Reader
{
    public interface IAppXmlReader
    {
        public string GetValueByKey(string keyToSearch);
    
    }
}
