namespace App.Config
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Data.Sqlite;
    using System.Data;
    using App.Enums;
    using App.Events;
    using App.Logic.IO.Handler;
    using App.Logic;

    public class AppInitialize
    {
        // TODO: statically set up Database name to "AppDb"
        // TODO: Split classess : data population, ddbb creation

        public static AppMessageHandler messageHandler = new AppMessageHandler();
        private const string MESSAGE_SENDER     = "AppInitialize";

        private const string INIT_DATABASE      = "Init SQL Server (1) , SqLite (2) , XML (3), Abort (A)";
        private const string CONFIRMATION       = "The database already exist, confirm deletion to continue ? (Y / N)";

        private const string MISSING_DATABASE   = "Must select 'SQL Server' or 'SqLite' type continue.";
        private const string MISSING_DISPLAY    = "Must select one Display output to continue.";
        private const string MISSING_DDBBCFG    = "Error while creating connection string to Database.";
        private const string NOT_IMPLEMENTED    = "Display output not implemented.";
        private const string DROPPING_DDBB      = "Droping SQL Server Database.";
        private const string DELETE_FILE_OK     = "Success deleting related Database Files : ";
        private const string DELETE_FILE_ERROR  = "Error deleting related Database Files : ";
        private const string CREATE_DDBB_ERROR  = "Error creating database ";
        private const string CREATE_PROPS_ERROR = "Error creating database properties ";
        private const string TASK               = "Process :";
        private const string TASK_DONE          = "done";
        private const string TASK_ERROR         = "error";
        private const string TASK_INPROGESS     = "in progress";


        private const string        dbName = "AppDb";
        private AppDDBBcfg          _appDDBBConfig;
        private DataBaseType        _dbType;
        private DisplaType          _displayType;
        //private static DisplayMessage Display;
        //private static UserAccess _userAccess;


        #region Constructors

        private AppInitialize() { }

        public AppInitialize(DataBaseType dataBaseType, DisplaType displaType)//, object? objDestination = null) 
        {
            this._dbType = dataBaseType;
            this._displayType = displaType;
            this._appDDBBConfig = new AppDDBBcfg(this._dbType, SqlServerMaster.No);
            //Display = new DisplayMessage(this._displayType, objDestination);
        }

        #endregion

        #region Properties

        public bool DDBBisInstalled
        {
            get { return File.Exists(this._appDDBBConfig.GetDatabaseFile); }
        }

        #endregion

        #region Event Handler & Exception : manageException() Send event with message and remove handler.
        private void manageException(string message)
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

        #region Confirmation Action
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
            //Display.Show("TODO : need to implement display for mobile and pc");

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
            //messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

            if (this._dbType == null)
                this.manageException(MISSING_DATABASE);

            if (this._displayType == null)
                this.manageException(MISSING_DISPLAY);
    
            if(this._appDDBBConfig == null)
                this.manageException(MISSING_DDBBCFG);

            bool ok = this.DDBBisInstalled;
            bool next = false;

            if (reInstall == true && ok == true)
                next = confirmAction(CONFIRMATION);

            if (next == true || ok == false)
                ok = initialize();

            //messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;

            return ok;
        }

        private bool initialize(bool reInstall = false)
        {

            bool ok = false;
            string databaseFolder   = this._appDDBBConfig.GetDatabasePath;
            string databaseFile     = this._appDDBBConfig.GetDatabaseFile;

            if (!Directory.Exists(databaseFolder))
                Directory.CreateDirectory(databaseFolder);

            try
            {
                if (File.Exists(databaseFile))
                {
                    if (this._dbType == DataBaseType.SqLite)
                        File.Delete(databaseFile);
                    else if (this._dbType == DataBaseType.SqlServer)
                    {
                        ok = dropDatabase();
                        messageHandler.updateStatus(MESSAGE_SENDER, DROPPING_DDBB, TASK_DONE);
                        File.Delete(databaseFile);
                        File.Delete(databaseFile.Replace(".mdf", ".ldf"));
                    }
                    messageHandler.updateStatus(MESSAGE_SENDER, DELETE_FILE_OK + this._dbType.ToString(), TASK_DONE);
                }
            }
            catch(Exception ex)
            {
                this.manageException(DELETE_FILE_ERROR + ex.Message);
            }

            messageHandler.updateStatus(MESSAGE_SENDER, "Initializing {" + this._dbType.ToString() + "}", TASK_INPROGESS);
            switch (this._dbType)
            {
                case DataBaseType.SqLite:
                    ok = initSqLiteDDBB(databaseFile);
                    break;
                case DataBaseType.SqlServer:
                    ok = initSQLServerDDBB(databaseFile.Replace(".mdf", ""), SqlServerMaster.Yes);
                    break;
                case DataBaseType.XML:
                    ok = false;
                    this.manageException(MISSING_DATABASE + ", {" + this._dbType.ToString() + "} not implemented.");
                    break;
            }
            messageHandler.updateStatus(MESSAGE_SENDER, "Database created {" + this._dbType.ToString() + "}" , TASK_DONE);

            if (ok)
                ok = initDB(this._dbType);

            if (ok == false)
                this.manageException(CREATE_DDBB_ERROR + this._dbType.ToString());

            return ok;
        }

        private bool dropDatabase()
        {
            this._appDDBBConfig.MasterDatabase = SqlServerMaster.Yes;
            using (SqlConnection conn = new SqlConnection(this._appDDBBConfig.GetConnectionString))
            {
                try
                {
                    conn.Open();
                    string sqlCommandText = @"ALTER DATABASE " + dbName + 
                        @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + dbName + "]";
                    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, conn);
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    this.manageException("Error " + DROPPING_DDBB + " " + dbName);
                }
                finally 
                { 
                    conn.Close(); 
                }
            }
            return true;
        }

        private bool initSQLServerDDBB(string fileName, SqlServerMaster master)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection();
            this._appDDBBConfig.MasterDatabase = master;
            conn.ConnectionString = this._appDDBBConfig.GetConnectionString;

            SqlCommand cmd = new SqlCommand(setDatabaseProps(fileName), conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                ok = true;
            }
            catch
            {
                this.manageException(CREATE_DDBB_ERROR + dbName);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ok;
        }

        private bool initSqLiteDDBB(string fileName)
        {
            bool ok = true;
            try
            {
                File.WriteAllBytes(fileName, new byte[0]);

                using (SqliteConnection conn = new SqliteConnection(this._appDDBBConfig.GetConnectionString))
                {
                    conn.Open();
                    conn.Close();
                };
            }
            catch
            { 
                ok = false;
                this.manageException(CREATE_DDBB_ERROR + dbName);
            }
            return ok;
        }

        private bool initDB(DataBaseType dbType)
        {
            bool ok = true;
            try
            {
                setInitData(dbType, setTableSchemas(dbType), "Creating");
                setInitData(dbType, setTableData(), "Populating");
            }
            catch(Exception ex)
            {
                ok = false;
                this.manageException(dbName + " error: " + ex.Message);
            }
            return ok;
        }

        private void setInitData(DataBaseType dbType, List<(string, string)> data, string title)
        {
            if (data.Count > 0)
            {
                switch (dbType)
                {
                    case DataBaseType.SqLite:
                        {
                            SqliteCommand SqLitecmd;
                            using (SqliteConnection SqLiteconn = new SqliteConnection(this._appDDBBConfig.GetConnectionString))
                            {
                                try
                                {
                                    SqLiteconn.Open();
                                    foreach ((string, string) dat in data)
                                    {
                                        SqLitecmd = new SqliteCommand(dat.Item2, SqLiteconn);
                                        SqLitecmd.ExecuteNonQuery();
                                        messageHandler.updateStatus(MESSAGE_SENDER, title + " table [" + dat.Item1 + "]", "done");
                                    }
                                }
                                finally
                                {
                                    SqLiteconn.Close();
                                }
                            }
                        }
                        break;
                    case DataBaseType.SqlServer:
                        {
                            SqlCommand SQLServercmd;
                            this._appDDBBConfig.MasterDatabase = SqlServerMaster.No;
                            using (SqlConnection SQLServerconn = new SqlConnection(this._appDDBBConfig.GetConnectionString))
                            {
                                try
                                {
                                    SQLServerconn.Open();
                                    foreach ((string, string) dat in data)
                                    {
                                        SQLServercmd = new SqlCommand(dat.Item2, SQLServerconn);
                                        SQLServercmd.ExecuteNonQuery();
                                        messageHandler.updateStatus(MESSAGE_SENDER, title + " table [" + dat.Item1 + "]", "done");
                                    }
                                }
                                finally
                                {
                                    SQLServerconn.Close();
                                }
                            }
                        }
                        break;
                }
            }
        }

        private string setDatabaseProps(string fileName)
        {
            string str = "CREATE DATABASE AppDb ON PRIMARY " +
                "(NAME = AppDb_Data, " +
                "FILENAME = '" + fileName + ".mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                "LOG ON (NAME = AppDb_Log, " +
                "FILENAME = '" + fileName + ".ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";
            return str;
        }

        private List<(string, string)> setTableSchemas(DataBaseType dbType)
        {
            bool _sqlite = (dbType == DataBaseType.SqLite) ? true : false;

            string _typeDb = (_sqlite == false ? "[dbo]." : "");
            string _typePk = (_sqlite == false ? "BIGINT NOT NULL PRIMARY KEY IDENTITY, " : "INTEGER PRIMARY KEY, ");
            string _typeInt = (_sqlite == false ? "BIGINT NOT NULL, " : "INTEGER NOT NULL, ");

            List<(string, string)> sql = new List<(string, string)>()
            {
                ("Users", "CREATE TABLE " + _typeDb + "[Users] ( " +
                "[Id] " + _typePk +
                "[Name] VARCHAR(50) NOT NULL, " +
                "[Surname] VARCHAR(100) NULL, " +
                "[Address] VARCHAR(100) NULL, " +
                "[CountryIdentity] VARCHAR(50) NOT NULL, " +
                "CONSTRAINT [uq_CountryIdentity] UNIQUE ([CountryIdentity]), " +
                "CONSTRAINT [uq_Users_FullName] UNIQUE ([Name], [Surname])) "),

                ("Tasks", "CREATE TABLE " + _typeDb + "[Tasks] ( " +
                "[Id] " + _typePk +
                "[Name] VARCHAR(100) NOT NULL, " +
                "[Observation] VARCHAR(100) NULL, " +
                "CONSTRAINT [uq_Tasks_Name] UNIQUE ([Name])) "),

                ("Permissions", "CREATE TABLE " + _typeDb + "[Permissions] ( " +
                "[Id] " + _typePk +
                "[Parent] VARCHAR(60) NOT NULL, " +
                "[Name] VARCHAR(60) NOT NULL, " +
                "[Observation] VARCHAR(500) NULL, " +
                "CONSTRAINT [uq_Permissions_Name] UNIQUE ([Name])) "),

                ("UsersLogin", "CREATE TABLE " + _typeDb + "[UsersLogin] ( " +
                "[Id] " + _typePk +
                "[UserId] BIGINT NOT NULL UNIQUE, " +
                "[UserName] NCHAR(10) NOT NULL, " +
                "[Password] NCHAR(10) NOT NULL, " +
                "CONSTRAINT [FK_UsersLogin_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), " +
                "CONSTRAINT [uq_UsersLogin_UserName] UNIQUE ([UserName])) "),

                ("UsersPermission", "CREATE TABLE " + _typeDb +  "[UsersPermission] ( " +
                "[Id] " + _typePk +
                "[UserId] " + _typeInt +
                "[PermissionId] " + _typeInt +
                "[userAccessType] " + _typeInt +
                "CONSTRAINT [uq_UsersPermission_UI_PI] UNIQUE([UserId], [PermissionId]), " +
                "CONSTRAINT FK_UsersPermission_Permissions FOREIGN KEY(PermissionId) " +
                "REFERENCES Permissions(Id), " +
                "CONSTRAINT FK_UsersPermission_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) "),

                ("UserTasks", "CREATE TABLE " + _typeDb + "[UserTasks] ( " +
                "[Id] " + _typePk +
                "[TaskId] " + _typeInt +
                "[UserId] " + _typeInt +
                "CONSTRAINT [uq_UserTasks_TI_UI] UNIQUE([TaskId], [UserId]), " +
                "CONSTRAINT FK_UserTasks_Tasks FOREIGN KEY(TaskId) " +
                "REFERENCES Tasks(Id), " +
                "CONSTRAINT FK_UserTasks_Users FOREIGN KEY(UserId) " +
                "REFERENCES Users(Id)) ")
            };
            return sql;
        }

        private List<(string, string)> setTableData()
        {
            List<(string, string)> data = new List<(string, string)>()
            {
                ("Users", "INSERT INTO Users(Name, Surname, Address, CountryIdentity) VALUES('admin', 'surname admin', 'address admin', 'ES-123456')"),
                ("Users", "INSERT INTO Users(Name, Surname, Address, CountryIdentity) VALUES('user1', 'surname 001', 'address 001', 'FR-123456')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Users', 'Table to storage Users personal data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UsersLogin', 'Relational table used to vinculate Users with log in data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Permissions', 'Table to storage Permissions data.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UsersPermission', 'Relational table used to vinculate users with modules.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'Tasks', 'Table to storge Tasks.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Maintenance', 'UserTasks', 'Relational table used to vinculate Users with Task.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Config', 'DAOSchemas', 'Class to get Table columns info.')"),
                ("Permissions", "INSERT INTO Permissions(Parent, Name, Observation) VALUES('Config', 'Export', 'Export Table data from database to XML.')"),
                ("Tasks", "INSERT INTO Tasks(Name, Observation) VALUES('First task', 'Task notes....1')"),
                ("Tasks", "INSERT INTO Tasks(Name, Observation) VALUES('Second task', 'Task notes....2')"),
                ("Users", "INSERT INTO UsersLogin(UserId, UserName, Password) VALUES(1, 'admin', '123')"),
                ("UsersLogin", "INSERT INTO UsersLogin(UserId, UserName, Password) VALUES(2, 'user 2', '123')"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 1, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 2, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 3, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 4, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 5, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 6, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(1, 7, 3)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 1, 1)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 2, 1)"),
                ("UsersPermission", "INSERT INTO UsersPermission(UserId, PermissionId, userAccessType) VALUES(2, 4, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(1, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(2, 1)"),
                ("UserTasks", "INSERT INTO UserTasks(TaskId, UserId) VALUES(1, 2)")
            };
            return data;
        }
    
    }
}
