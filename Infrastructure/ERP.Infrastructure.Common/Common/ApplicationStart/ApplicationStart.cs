using ERP.Application.Auth;
using ERP.Application.Interfaces.LoginService;
using ERP.Application.Interfaces.Persistence;
using ERP.Application.Services;
using ERP.Domain.Entity.AppConfig;
using ERP.Domain.Interfaces.Config;
using ERP.Domain.Interfaces.Security;
using ERP.Infrastructure.Common.Security;
using ERP.Infrastructure.Services;
using ERP.Infrastructure.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ERP.Infrastructure.Start;

internal sealed class ApplicationStart
{
    private readonly IServiceCollection _services;
    
    public ApplicationStart(IServiceCollection services) // El constructor recibe la lista oficial del Main
    {
        this._services = services;
    }

    public void Initialize()
    {
        this._services.AddSingleton<IEnvironmentContext, WindowsEnvironmentContext>();
        this._services.AddSingleton<ISessionContext, SessionContext>();

        // Registrar el lector de configuración
        this._services.AddTransient<ApplicationSettings>();

        // Leer el JSON una sola vez y registrar la misma instancia para ambas interfaces
        this._services.AddSingleton<AppConfigEntity>(provider =>
        {
            var settings = provider.GetRequiredService<ApplicationSettings>();
            return settings.GetValues();
        });

        this._services.AddSingleton<IDatabaseConfig>(provider => provider.GetRequiredService<AppConfigEntity>());
        this._services.AddSingleton<IStorageConfig>(provider => provider.GetRequiredService<AppConfigEntity>());

        // Registrar la conexión
        this._services.AddSingleton<IConnectionStringService, SqliteConnectionStringService>();

        // Registrar la Persistencia
        this._services.AddTransient<IAuthUserReadRepository, SqliteAuthUserRepository>();
        this._services.AddTransient<IUserWriteRepository, SqliteUserWriteRepository>();

        // Registrar la Lógica/Aplicación sin errores de variables
        this._services.AddTransient<ILoginService, LoginService>();
        /*
        this._services.AddTransient<ILoginService, LoginService>(provider =>
        {
            var repo = provider.GetRequiredService<IAuthUserReadRepository>();
            return new LoginService(repo);
        });
        */
    }
}