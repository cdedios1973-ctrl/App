using ERP.Domain.Entity.AppConfig;
using ERP.Infrastructure.Services.Files;
using ERP.Infrastructure.Start;
using ERP.View.Presenter.Interfaces;
using ERP.View.Presenter.Login;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
//using ERP.Domain.Interfaces.Config;
//using ERP.Application.Services;
//using ERP.Application.Interfaces.Persistence;
//using ERP.Application.Interfaces.LoginService;
//using ERP.Application.Interfaces.LoginService;
//using ERP.Infrastructure.Sqlite;
//using ERP.Infrastructure.Config;
//using ERP.Infrastructure.Common.Security;

namespace ERP.Views.WinForms.UI
{
    internal static class Program
    {
        [STAThread]
        //static void Main(string[] args)
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Configuración de servicios, BuildServiceProvider() y Application.Run(mainForm)

            try
            {
                // 1. Crear el contenedor de servicios
                var services = new ServiceCollection();

                // 2. Common registra la conexión, repositorios y el ILoginService
                new ApplicationStart(services).Initialize();

                // 3. Registrar componentes de la interfaz de usuario
                services.AddTransient<LoginPresenter>();

                // Registrar el formulario como Singleton (al ser la ventana principal) y enlazar la interfaz ILoginView para que apunte a esa MISMA instancia:
                services.AddSingleton<FrmLoginService>();
                services.AddSingleton<ILoginView>(provider => provider.GetRequiredService<FrmLoginService>());

                // 4. Construir el proveedor
                using var serviceProvider = services.BuildServiceProvider();





                // 4.5 Verificar que la base de datos existe
                var fileChecker = serviceProvider.GetRequiredService<IFileSystemChecker>();
                var dbConfig = serviceProvider.GetRequiredService<IDatabaseConfig>();

                if (!fileChecker.FileExists(dbConfig.LocalDatabasePath, dbConfig.DatabaseName))
                {
                    throw new FileNotFoundException(
                        $"No se encontró la base de datos '{dbConfig.DatabaseName}' en la ruta '{dbConfig.LocalDatabasePath}'.");
                }






                // 5. Arrancar la aplicación
                var mainForm = serviceProvider.GetRequiredService<FrmLoginService>();
                // Al pedir el Presenter, inyectará automáticamente el mismo 'mainForm'
                var loginPresenter = serviceProvider.GetRequiredService<LoginPresenter>();

                System.Windows.Forms.Application.Run(mainForm);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error crítico al iniciar la aplicación:\n{ex.Message}",
                    "Error de sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /*
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Crear el contenedor de servicios
            var services = new ServiceCollection();

            // 2. Registrar los componentes: INFRAESTRUCTURA: Necesaria para que los repositorios sepan dónde está la DB
            // services.AddSingleton<IConnectionStringService, ImplementacionConnectionStringService>();
            // PERSISTENCIA (Infrastructure) -> Mantenemos Scoped/Singleton para reutilizar conexiones
            services.AddScoped<IAuthUserReadRepository, SqliteAuthUserRepository>();
            services.AddScoped<IUserWriteRepository, SqliteUserWriteRepository>();
            // LÓGICA / APLICACIÓN (Logic) -> ¡Esta es la que te faltaba registrar!
            //services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginService, LoginService>(provider =>
            {
                var repo = provider.GetRequiredService<IAuthUserReadRepository>();
                return new LoginService(repo);
            });
            // CONTROLADOR (Para web / stateless)
            //services.AddScoped<ILoginController, LoginController>();
            // PRESENTER (Para Windows forms)
            services.AddTransient<LoginPresenter>();
            // VISTAS (Presentation UI) -> Siempre de tipo Transient
            // Registra la vista tanto por su clase concreta como por su interfaz ILoginView
            services.AddTransient<FrmLoginService>();

            // 3. Construir el proveedor
            using var serviceProvider = services.BuildServiceProvider();

            // 4. Arrancar la aplicación
            var mainForm = serviceProvider.GetRequiredService<FrmLoginService>();
            var loginService = serviceProvider.GetRequiredService<ILoginService>();

            // 5. Presenter manual pasándole exactamente esa instancia de mainForm
            var loginPresenter = new LoginPresenter(mainForm, loginService);

            System.Windows.Forms.Application.Run(mainForm);
        }
        */
    }
}
