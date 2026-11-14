using ERP.Application.Auth;
using ERP.Application.Interfaces.LoginService;
using ERP.Application.Interfaces.Persistence;
using ERP.Application.Services;
using ERP.Domain.Entity.AppConfig;
using ERP.Domain.Interfaces.Config;
using ERP.Domain.Interfaces.Security;
using ERP.Infrastructure.Common.Security;
using ERP.Infrastructure.Services;
using ERP.Infrastructure.Services.Files;
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
        // 1. Herramientas básicas (se crean una vez y viven siempre)
        this._services.AddSingleton<IFileSystemChecker, LocalFileSystemChecker>();
        this._services.AddSingleton<IEnvironmentContext, WindowsEnvironmentContext>();
        this._services.AddSingleton<ISessionContext, SessionContext>();

        // 2. Configuración (lee el JSON una vez)
        this._services.AddSingleton<ApplicationSettings>();
        this._services.AddSingleton<IDatabaseConfig>(p => p.GetRequiredService<ApplicationSettings>().GetValues());
        this._services.AddSingleton<IStorageConfig>(p => p.GetRequiredService<ApplicationSettings>().GetValues());

        // 3. Base de datos y repositorios
        this._services.AddSingleton<IConnectionStringService, SqliteConnectionStringService>();
        this._services.AddTransient<IAuthUserReadRepository, SqliteAuthUserRepository>();
        this._services.AddTransient<IUserWriteRepository, SqliteUserWriteRepository>();

        // 4. Servicios de negocio
        this._services.AddTransient<ILoginService, LoginService>();
    }

}