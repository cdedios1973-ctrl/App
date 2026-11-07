using System;

namespace ERP.Infrastructure.Reader;
public interface IAppXmlReader
{
    public string GetValueByKey(string keyToSearch);

}
