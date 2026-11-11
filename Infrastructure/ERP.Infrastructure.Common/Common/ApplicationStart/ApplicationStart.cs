using System;
using Microsoft.Extensions.DependencyInjection;

//using ERP.Domain.Interfaces.Config;
using ERP.Application.Services;
using ERP.Application.Interfaces.Persistence;
using ERP.Application.Interfaces.LoginService;
using ERP.Infrastructure.Sqlite;

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
        // 1. Registrar la conexión
        //this._services.AddSingleton<IConnectionStringService>(new SqliteConnectionStringService("Data Source=app.db;"));

        // 2. Registrar la Persistencia
        this._services.AddScoped<IAuthUserReadRepository, SqliteAuthUserRepository>();
        this._services.AddScoped<IUserWriteRepository, SqliteUserWriteRepository>();

        // 3. Registrar la Lógica/Aplicación sin errores de variables
        this._services.AddScoped<ILoginService, LoginService>(provider =>
        {
            var repo = provider.GetRequiredService<IAuthUserReadRepository>();
            return new LoginService(repo);
        });
    }
}