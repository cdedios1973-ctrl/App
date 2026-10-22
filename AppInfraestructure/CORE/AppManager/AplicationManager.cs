using System;
using App.Definitions.Enums;

namespace App.CORE
{
    internal class AplicationManager
    {
        internal static AplicationManager manager = null;
        private static readonly object _isObject = new object();

        //private DataBaseType _currentDatabase;

        #region Constructor

        private AplicationManager()
        {
            // Inicializar base de datos, logs, etc.
            //this._currentDatabase = DataBaseType.SqLite;
        }

        #endregion

        #region Singleton Instance

        internal static AplicationManager Instance
        {
            get
            {
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

        #endregion

        // 3. Métodos de negocio (instancia normal, no estáticos)
        internal void InitializeConfiguration()
        {
            // TODO : no need try / catch, already done at program.cs
            // Capturing events, show information to user at screen
            //DisplayMessage.messageHandler.UpdateStatus += Program.MessageHandler_UpdateStatus;
            // Call Service AppGetConnectionString (done, need implement line)
            //string connstr = new AppConfigConnectionString(this._currentDatabase).GetConnectionString();

            // PSEUDO : If (database.open == errror) > create database (done at appinitilize, need refactoring  also, use scripts, remove from code)
            //AppInitialize.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

        }

        internal bool StartMainThread()
        {
            //Console.WriteLine("Ejecutando la lógica principal del programa...");
            return false;
        }

        internal void CloseMainThread()
        {
            //DisplayMessage.messageHandler.UpdateStatus -= Program.MessageHandler_UpdateStatus;
            //AppInitialize.messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
        }
    }
}
