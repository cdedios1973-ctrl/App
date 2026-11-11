using System;
using Microsoft.Extensions.Configuration;
using ERP.Domain.Entity.AppConfig;
using ERP.Infrastructure.Services.Interfaces;

namespace ERP.Infrastructure.Services;

internal sealed class ApplicationSettings
{
    // TODO : la ruta al archivo de configuracion
    private readonly string _appDirectory = Directory.GetCurrentDirectory();

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
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
        // reloadOnChange: true

        string connection   = config.GetConnectionString("SqliteConnection");
        string xmlPath      = config["LocalXMLPath"];
        string dbName       = config["DatabaseName"];
        string dbPath       = config["LocalDatabasePath"];
        string dbNetwork    = config["NetworkDatabasePath"];

        AppConfigEntity data = new AppConfigEntity(new ConnectionStringsConfig(connection) , dbName,dbPath,dbNetwork, xmlPath);

        return data;
    }
}
