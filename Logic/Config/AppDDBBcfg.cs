namespace App.Config
{
    using Microsoft.Data.SqlClient;
    using App.Enums;
    //using App.Logic;

    public class AppDDBBcfg
    {
        //private static UserAccess _userAccess;
        private string _connectionString;
        private string _databasePath;
        private string _databaseFile;

        private DataBaseType    _dbType;
        private SqlServerMaster _master;

        public AppDDBBcfg(DataBaseType dataBaseType, SqlServerMaster master = SqlServerMaster.No)
        {
            this._dbType = dataBaseType;
            this._master = master;
            if (dataBaseType == DataBaseType.SqlServer)
                this._connectionString = getSqlServerConnectionString;
            else if (dataBaseType == DataBaseType.SqLite)
                this._connectionString = "Data Source=" + GetDatabaseFile;
        }

        /*
        public AppDDBBcfg(UserAccess userAccess, SqlServerMaster master = SqlServerMaster.No )
        {
            _userAccess = userAccess;
            //this._connection.ConnectionString = ConfigurationManager.ConnectionStrings["AppDb"].ToString();
            this._master = master;
            if (_userAccess.DDBBType == DataBaseType.SqlServer)
                this._connectionString = getSqlServerConnectionString;
            else if (_userAccess.DDBBType == DataBaseType.SqLite)
                this._connectionString = "Data Source=" + GetDatabaseFile;
        }
        */

        #region Properties

        public SqlServerMaster MasterDatabase
        { 
            get { return this._master; }
            set 
            { 
                this._master = value;
                this._connectionString = this.getSqlServerConnectionString;
            }
        }
        
        public string GetDatabasePath
        {
            get 
            {
                if (this._databasePath == null)
                {
                    string tmp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    this._databasePath = Path.Combine(tmp, "Database");
                    //this._databasePath = Path.Combine(AppContext.BaseDirectory, "Database");

                }
                return this._databasePath; 
            }
        }

        public string GetDatabaseFile
        {
            get 
            {
                if (this._databaseFile == null)
                    this._databaseFile = Path.Combine(this.GetDatabasePath, dbFileName);
                return this._databaseFile;
            }
        }

        private string dbFileName
        {
            get 
            {
                string dbName = "AppDbData";
                switch (this._dbType)
                {
                    case DataBaseType.SqLite:
                        dbName += ".sqlite";
                        break;
                    case DataBaseType.SqlServer:
                        dbName += ".mdf";
                        break;
                }
                return dbName;
            }
        }

        public string GetConnectionString
        {
            get { return this._connectionString; }
        }

        private string getSqlServerConnectionString
        {
            get {
                return new SqlConnectionStringBuilder()
                {
                    DataSource = "(localdb)\\MSSQLLocalDB",
                    InitialCatalog = (this._master == SqlServerMaster.Yes) ? "master" : "AppDb",
                    IntegratedSecurity = true,
                    PersistSecurityInfo = false,
                    Pooling = false,
                    Encrypt = true,
                    TrustServerCertificate = true,
                    CommandTimeout = 0
                }.ConnectionString;
            }
        }

        #endregion

    }
}
