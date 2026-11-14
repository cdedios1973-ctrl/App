using System;
using System.IO;
using ERP.Domain.Entity.AppConfig;
using ERP.Domain.Interfaces.Security;
//using ERP.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ERP.Infrastructure.Services;

internal sealed class ApplicationSettings
{
    private readonly string _appPath = Path.Combine(AppContext.BaseDirectory, "config");

    public ApplicationSettings(IEnvironmentContext environmentContext)
    { 
        if(environmentContext == null)
            throw new ArgumentNullException(nameof(environmentContext));
        
        if(string.IsNullOrEmpty(environmentContext.GetCurrentUserIdentity()))
            throw new UnauthorizedAccessException("Current user has no permission to lauch application");

    }

    internal AppConfigEntity GetValues()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(_appPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)// reloadOnChange: true
            .Build();

        string connection = GetRequiredSetting(config.GetConnectionString("SqliteConnection"), "SqliteConnection");
        string dbName = GetRequiredSetting(config["DatabaseName"], "DatabaseName");
        string dbPath = GetRequiredSetting(config["LocalDatabasePath"], "LocalDatabasePath");
        string dbNetworkPath = GetRequiredSetting(config["NetworkDatabasePath"], "NetworkDatabasePath");
        string xmlPath = GetRequiredSetting(config["LocalXMLPath"], "LocalXMLPath");

        return new AppConfigEntity(
            new ConnectionStringsConfig(connection),
            dbName,
            dbPath,
            dbNetworkPath,
            xmlPath);
    }

    private static string GetRequiredSetting(string? value, string keyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Missing or empty configuration setting: '{keyName}'.");

        return value;
    }
}
