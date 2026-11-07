using System;

/// <summary>
/// POCO con la estructura del archivo de configuracion.
/// </summary>
namespace ERP.Domain.Entity.AppConfig;

public sealed record class AppConfigEntity
{
    public ConnectionStringsConfig ConnectionStrings { get; init; }
    public string DatabaseName { get; init; }
    public string LocalDatabasePath { get; init; }
    public string NetworkDatabasePath { get; init; }
    public string LocalXMLPath { get; init; }

    public AppConfigEntity(ConnectionStringsConfig connectionStrings, string databaseName, string localDatabasePath, string networkDatabasePath, string localXMLPath)
    {
        this.ConnectionStrings = connectionStrings;
        this.DatabaseName = databaseName;
        this.LocalDatabasePath = localDatabasePath;
        this.NetworkDatabasePath = networkDatabasePath;
        this.LocalXMLPath = localXMLPath;
    }
}

public sealed record class ConnectionStringsConfig
{
    public string SqliteConnectionString { get; init; }

    public ConnectionStringsConfig(string connectionString)
    {
        this.SqliteConnectionString = connectionString;
    }
}
