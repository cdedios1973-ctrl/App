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

namespace ERP.Views.WinForms.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize(); // ¡No olvides la configuración nativa de WinForms!

            // 1. Crear el contenedor de servicios
            var services = new ServiceCollection();

            // 2. Tu capa Common registra la conexión, repositorios y el ILoginService
            new ApplicationStart(services).Initialize();

            // 3. Registramos los componentes de la interfaz de usuario
            services.AddTransient<LoginPresenter>();
            services.AddTransient<FrmLoginService>();

            // 4. Construir el proveedor
            using var serviceProvider = services.BuildServiceProvider();

            // 5. Arrancar la aplicación pidiendo únicamente la pantalla
            var mainForm = serviceProvider.GetRequiredService<FrmLoginService>();
            var loginPresenter = serviceProvider.GetRequiredService<LoginPresenter>();

            System.Windows.Forms.Application.Run(mainForm);
        }

        /*
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Crear el contenedor de servicios
            var services = new ServiceCollection();

            // 2. Registrar los componentes
            // INFRAESTRUCTURA: Necesaria para que los repositorios sepan dónde está la DB
            // TODO: Cambia 'TuImplementacionConnectionStringService' por tu clase real que lee el json
            // services.AddSingleton<IConnectionStringService, TuImplementacionConnectionStringService>();

            // PERSISTENCIA (Infrastructure) -> Mantenemos Scoped/Singleton para reutilizar conexiones
            services.AddScoped<IAuthUserReadRepository, SqliteAuthUserRepository>();
            services.AddScoped<IUserWriteRepository, SqliteUserWriteRepository>();

            // LÓGICA / APLICACIÓN (Logic) -> ¡Esta es la que te faltaba registrar!
            //services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginService, LoginService>(provider =>
            {
                // Como Program.cs está en el proyecto visual, y tiene el 'InternalsVisibleTo',
                // AQUÍ SÍ puedes hacer el 'new' de tu constructor internal sin problemas.
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
