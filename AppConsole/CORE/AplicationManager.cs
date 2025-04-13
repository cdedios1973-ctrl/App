using System;
using App.Definitions.Enums;
using App.Logic;
using App.Logic.Services.Commands;
using App.Logic.Services.Config;
using App.Logic.Services.Input;

namespace AppConsole.CORE
{
    // Clase que implementa el Patrón Singleton (Thread-Safe básico para Framework)
    internal class AplicationManager
    {
        internal static AplicationManager manager = null;
        private static readonly object _isObject = new object();

        private AppInputService _appMenuHandler;
        private DataBaseType _currentDatabase;

        // 1. Constructor privado: nadie fuera de esta clase puede hacer un "new"
        private AplicationManager()
        {
            // Inicializar base de datos, logs, etc.
            this._currentDatabase = DataBaseType.SqLite;
        }

        // 2. Propiedad pública estática para obtener la única instancia
        internal static AplicationManager Instance
        {
            get
            {
                // Bloqueo para evitar problemas si usas hilos (Thread-Safe)
                lock (_isObject)
                {
                    if (manager == null)
                    {
                        manager = new AplicationManager();
                    }
                    return manager;
                }
            }
        }

        // 3. Métodos de negocio (instancia normal, no estáticos)
        internal void InitializeConfiguration()
        {
            Console.WriteLine("Cargando parámetros desde el Singleton...");
            // TODO : Move this block to Logic -> can be used for other platforms (call to AppServiceInitialize) for program.messagehander, pass object to class and assing there
            // TODO : no need try / catch, already done at program.cs
            // Capturing events, show information to user at screen
            DisplayMessage.messageHandler.UpdateStatus += Program.MessageHandler_UpdateStatus;
            // Call Service AppGetConnectionString (done, need implement line)
            string connstr = new AppConfigConnectionString(this._currentDatabase).GetConnectionString();

            // PSEUDO : If (database.open == errror) > create database (done at appinitilize, need refactoring  also, use scripts, remove from code)
            AppInitialize.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

        }

        internal bool StartMainThread()
        {
            Console.WriteLine("Ejecutando la lógica principal del programa...");

            _appMenuHandler.Input("M"); // TODO : change AppMenuHandler class to AppInputService at logic to show first menu

            AppCommandLogin login = new AppCommandLogin();
            while (true)
                if (login.LogIn(Console.ReadLine() + "") == true)
                    return true;
        }

        internal void CloseMainThread()
        {
            DisplayMessage.messageHandler.UpdateStatus -= Program.MessageHandler_UpdateStatus;
            AppInitialize.messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
        }
    }
}
