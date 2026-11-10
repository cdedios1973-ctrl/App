using System;

namespace ERP.Infrastructure.Services.Interfaces;
public interface IAppXmlReader
{
    public string GetValueByKey(string keyToSearch);

}
