using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using ERP.Domain.Entity.AppConfig;
using ERP.Infrastructure.Services.Interfaces;

namespace ERP.Infrastructure.Services;

internal sealed class ApplicationSettings
{
    private readonly string _appPath = Path.Combine(AppContext.BaseDirectory, "config");

    internal ApplicationSettings(IUserContext currentUser)
    { 
        if(currentUser == null)
            throw new ArgumentNullException(nameof(currentUser));
        
        if(string.IsNullOrEmpty(currentUser.GetUserRole()))
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
