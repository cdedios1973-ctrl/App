using System;

namespace App.Definitions.Interfaces.Config
{
    public interface IConnectionStringService
    {
        string GetConnectionString(string dbName);
    }
}
