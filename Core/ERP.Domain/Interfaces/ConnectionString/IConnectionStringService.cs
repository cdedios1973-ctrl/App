using System;

namespace ERP.Domain.Interfaces.Config;

public interface IConnectionStringService
{
    string GetConnectionString();
}
