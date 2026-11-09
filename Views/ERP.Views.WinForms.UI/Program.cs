using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

using ERP.Controller.Login;
using ERP.Application.Services.Security;
using ERP.Application.Interfaces.Persistence;
using ERP.Domain.Interfaces.Controller;
using ERP.Domain.Interfaces.Login;
using ERP.Domain.Interfaces.Config;
using ERP.Infrastructure.Sqlite;
// using ERP.Infrastructure.Config; // Importa tu implementación del ConnectionString si existe

namespace ERP.Views.WinForms.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Crear el contenedor de servicios
            var services = new ServiceCollection();

            // 2. Registrar los componentes (El "Pegamento" del Arquitecto)

            // INFRAESTRUCTURA: Necesaria para que los repositorios sepan dónde está la DB
            // TODO: Cambia 'TuImplementacionConnectionStringService' por tu clase real que lee el json
            // services.AddSingleton<IConnectionStringService, TuImplementacionConnectionStringService>();

            // PERSISTENCIA (Infrastructure) -> Mantenemos Scoped/Singleton para reutilizar conexiones
            services.AddScoped<IAuthUserReadRepository, SqliteAuthUserRepository>();
            services.AddScoped<IUserWriteRepository, SqliteUserWriteRepository>();

            // LÓGICA / APLICACIÓN (Logic) -> ¡Esta es la que te faltaba registrar!
            //services.AddScoped<ILoginService, LoginService>();
            // Cambia la línea del LoginService por esta:
            services.AddScoped<ILoginService, LoginService>(provider =>
            {
                // Como Program.cs está en el proyecto visual, y tiene el 'InternalsVisibleTo',
                // AQUÍ SÍ puedes hacer el 'new' de tu constructor internal sin problemas.
                var repo = provider.GetRequiredService<IAuthUserReadRepository>();
                return new LoginService(repo);
            });

            // CONTROLADOR (Presentation Core)
            services.AddScoped<ILoginController, LoginController>();

            // VISTAS (Presentation UI) -> Siempre de tipo Transient
            services.AddTransient<FrmLoginService>();

            // 3. Construir el proveedor
            using var serviceProvider = services.BuildServiceProvider();

            // 4. Arrancar la aplicación desde el contenedor
            var mainForm = serviceProvider.GetRequiredService<FrmLoginService>();
            System.Windows.Forms.Application.Run(mainForm);
        }
    }
}
