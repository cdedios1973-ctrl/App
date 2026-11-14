using System;

using ERP.Domain.Entity.AppConfig;
using ERP.Domain.Interfaces.Config;

namespace ERP.Infrastructure.Services;

public class SqliteConnectionStringService : IConnectionStringService
{
    private readonly IDatabaseConfig _dbConfig;

    public SqliteConnectionStringService(IDatabaseConfig dbConfig)
    {
        _dbConfig = dbConfig;
    }

    public string GetConnectionString() 
    {
        return this._dbConfig.ConnectionStrings.SqliteConnectionString;
    }
}
