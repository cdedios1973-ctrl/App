namespace App.Config.DDBB
{
    using App.Definitions.Enums;
    using App.Definitions.Events;
    //using App.Logic;
    //using App.Logic.IO.Handler;

    internal class AppInitialize
    {
        // TODO: Split classess : data population (done), ddbb creation, and all clases.... split Actions.

        public static AppMessageHandler messageHandler = new AppMessageHandler();

        internal const string MESSAGE_SENDER     = "AppInitialize";
        private const string INIT_DATABASE      = "Init SQL Server (1) , SqLite (2) , XML (3), Abort (A)";
        private const string CONFIRMATION       = "The database already exist, confirm deletion to continue ? (Y / N)";
        private const string MISSING_DATABASE   = "Must select 'SQL Server' or 'SqLite' type continue.";
        private const string MISSING_DISPLAY    = "Must select one Display output to continue.";
        private const string MISSING_DDBBCFG    = "Error while creating connection string to Database.";
        private const string NOT_IMPLEMENTED    = "Display output not implemented.";
        internal const string DROPPING_DDBB      = "Droping SQL Server Database.";
        private const string DELETE_FILE_OK     = "Success deleting related Database Files : ";
        internal const string DELETE_FILE_ERROR  = "Error deleting related Database Files : ";
        internal const string CREATE_DDBB_ERROR  = "Error creating database ";
        private const string CREATE_PROPS_ERROR = "Error creating database properties ";
        private const string TASK               = "Process :";
        private const string TASK_DONE          = "done";
        private const string TASK_ERROR         = "error";
        private const string TASK_INPROGESS     = "in progress";

        internal const string               _dbName = "AppDb";
        internal static AppDDBBcfg          _appDDBBConfig;
        internal static AppDBSchema         _appDBSchema;
        internal static AppSQLServerCreate  _SqlServer;
        internal static AppSQLiteCreate     _SqLite;
        private DataBaseType                _dbType;
        private DisplaType                  _displayType;
        //private static UserAccess _userAccess;



        // TODO:
        // If user initializr for first time, no permissions needed
        // else look for admin privilegies (for testing pourposes)
        // not doable and remove this option before production.


        #region Constructors

        //internal AppInitialize() { }

        public AppInitialize(DataBaseType dataBaseType, DisplaType displaType) 
        {
            this._dbType        = dataBaseType;
            this._displayType   = displaType;
            _appDDBBConfig      = new AppDDBBcfg(this._dbType, SqlServerMaster.No);
            _appDBSchema        = new AppDBSchema();

            if(dataBaseType == DataBaseType.SqlServer)
                _SqlServer          = new AppSQLServerCreate();
            else if(dataBaseType == DataBaseType.SqLite)
                _SqLite             = new AppSQLiteCreate();
        }

        #endregion

        #region Properties

        public bool DDBBisInstalled
        {
            get { return File.Exists(_appDDBBConfig.GetDatabaseFile); }
        }

        #endregion

        #region Event Handler & Exception : manageException() Send event with message.
        internal void manageException(string message)
        {
            messageHandler.updateStatus(MESSAGE_SENDER, message, TASK_ERROR);
            //messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
            throw new Exception(message);
        }

        /*
        private void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            if (this._displayType == DisplaType.Console)
            {
                UpdateStatusEventArgs use = (UpdateStatusEventArgs)e;
                string output = TASK + use.Message + " -> " + use.Status;
                Console.WriteLine(output);
            }
        }
        */

        #endregion

        #region Install : Confirmation Action

        private bool confirmAction(string message)
        {
            bool next = true;

            switch (this._displayType)
            {
                case DisplaType.Console:
                    messageHandler.updateStatus(MESSAGE_SENDER, message, "?");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    next = (key.Key == ConsoleKey.Y) ? true : false;
                    break;
                case DisplaType.Mobile:
                    next = true;
                    break;
                default:
                    this.manageException(NOT_IMPLEMENTED);
                    break;
            }

            return next;
        }

        #endregion

        public DataBaseType Initialize()
        {
            //TODO : carefull : user can delete all data, also no need permissions.
            //Display.Show("TODO : need to implement display for pc");

            DataBaseType dbType = _dbType;

            switch (this._displayType)
            {
                case DisplaType.Console:
                    {
                        DisplayMessage.Show(MESSAGE_SENDER, INIT_DATABASE);
                        ConsoleKey k = Console.ReadKey().Key;
                        while (k != ConsoleKey.A)
                        {
                            dbType = InputString.CheckSource(k);
                            if (dbType != DataBaseType.Class)
                                break;
                        }
                        break;
                    }
                case DisplaType.Mobile:
                    {
                        dbType = DataBaseType.SqLite;
                        break;
                    }
                case DisplaType.Desktop:
                    {
                        this.manageException(NOT_IMPLEMENTED + DisplaType.Desktop.ToString());
                        break;
                    }
            }

            if (dbType == DataBaseType.SqLite || dbType == DataBaseType.SqlServer)
            {
                this._dbType = dbType;
                bool ok = this.Init(true);
                DisplayMessage.Show(MESSAGE_SENDER, ok == true ? "Successfully initializated." : TASK_ERROR);
            }

            return dbType;
        }

        private bool Init(bool reInstall = false)
        {

            if (this._dbType == null)
                this.manageException(MISSING_DATABASE);

            if (this._displayType == null)
                this.manageException(MISSING_DISPLAY);
    
            if(_appDDBBConfig == null)
                this.manageException(MISSING_DDBBCFG);

            bool ok = this.DDBBisInstalled;
            bool next = false;

            if (reInstall == true && ok == true)
                next = confirmAction(CONFIRMATION);

            if (next == true || ok == false)
                ok = initialize();

            return ok;
        }

        private bool initialize(bool reInstall = false)
        {
            bool ok = false;
            string databaseFolder = _appDDBBConfig.GetDatabasePath;
            string databaseFile = _appDDBBConfig.GetDatabaseFile;

            if (!Directory.Exists(databaseFolder))
                Directory.CreateDirectory(databaseFolder);

            messageHandler.updateStatus(MESSAGE_SENDER, "Initializing {" + this._dbType.ToString() + "}", TASK_INPROGESS);

            switch (this._dbType)
            {
                case DataBaseType.SqLite:
                    {
                        ok = _SqLite.SQLiteDeleteFiles(databaseFile);
                        messageHandler.updateStatus(MESSAGE_SENDER, DELETE_FILE_OK + this._dbType.ToString(), TASK_DONE);

                        ok = _SqLite.SQLiteCreate(databaseFile);
                        ok = _SqLite.SQLiteCreateTables();
                        break;
                    }
                case DataBaseType.SqlServer:
                    {
                        ok = _SqlServer.SQLServerDrop();
                        messageHandler.updateStatus(MESSAGE_SENDER, DROPPING_DDBB, TASK_DONE);

                        ok = _SqlServer.SQLServerDeleteFiles(databaseFile);
                        messageHandler.updateStatus(MESSAGE_SENDER, DELETE_FILE_OK + this._dbType.ToString(), TASK_DONE);

                        ok = _SqlServer.SQLServerCreate(databaseFile.Replace(".mdf", ""), SqlServerMaster.Yes);
                        ok = _SqlServer.SQLServerCreateTables();
                        break;
                    }
                case DataBaseType.XML:
                    ok = false;
                    this.manageException(MISSING_DATABASE + ", {" + this._dbType.ToString() + "} not implemented.");
                    break;
            }

            if (ok == false)
                this.manageException(CREATE_DDBB_ERROR + this._dbType.ToString());
            else
                messageHandler.updateStatus(MESSAGE_SENDER, "Database created {" + this._dbType.ToString() + "}", TASK_DONE);

            return ok;
        }

    }
}
