using ERP.Domain.Entity.AppConfig;
using ERP.Domain.Interfaces.Security;
using ERP.Infrastructure.Services.Files;
//using ERP.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ERP.Infrastructure.Services;

internal sealed class ApplicationSettings
{
    private readonly string _appPath = Path.Combine(AppContext.BaseDirectory); //, "config");
    private readonly IFileSystemChecker _fileSystemChecker;

    public ApplicationSettings(IEnvironmentContext environmentContext, IFileSystemChecker fileSystemChecker)
    { 
        if(environmentContext == null)
            throw new ArgumentNullException(nameof(environmentContext));
        
        if(string.IsNullOrEmpty(environmentContext.GetCurrentUserIdentity()))
            throw new UnauthorizedAccessException("Current user has no permission to lauch application");

        _fileSystemChecker = fileSystemChecker ?? throw new ArgumentNullException(nameof(fileSystemChecker));

    }

    internal AppConfigEntity GetValues()
    {
        if (!_fileSystemChecker.FileExists(_appPath, "appsettings.json"))
            throw new FileNotFoundException("No se encontró el archivo de configuración.", Path.Combine(_appPath, "appsettings.json"));

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(_appPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)// reloadOnChange: true
            .Build();

        string connection = GetRequiredSetting(config.GetConnectionString("SqliteConnection"), "SqliteConnection");

        return new AppConfigEntity(
            new ConnectionStringsConfig(connection),
            databaseName        : GetRequiredSetting(config["DatabaseName"], "DatabaseName"),
            localDatabasePath   : GetRequiredSetting(config["LocalDatabasePath"], "LocalDatabasePath"),
            networkDatabasePath : GetRequiredSetting(config["NetworkDatabasePath"], "NetworkDatabasePath"),
            localXMLPath        : GetRequiredSetting(config["LocalXMLPath"], "LocalXMLPath"));
    }

    private static string GetRequiredSetting(string? value, string keyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Missing or empty configuration setting: '{keyName}'.");

        return value;
    }
}
