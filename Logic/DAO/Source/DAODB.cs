namespace App.DAO.Source
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using System.Reflection;
    using System.Collections.Specialized;

    //using System.Configuration;
    using Microsoft.Data.SqlClient;
    using Microsoft.Data.Sqlite;

    using App.Models;
    using App.Enums;
    using App.Interfaces;
    using App.Logic;
    using App.Config;


    // TODO: check if user has permissions <- just implement modules id
    // TODO: DDBB cstring get from ConfigurationManager (issues): DataSource = "(localdb)\\MSSQLLocalDB" &  InitialCatalog = "AppDb" 
    // TODO: DbProviderFactory -> sql server (issues with IDE version)
    // TODO: sql update process
    // TODO: multi-database
    // TODO: GetConnectionString from app.config also updade command
    // TODO: return last id (not working with this version of sql)

    internal sealed class DAODB<T> where T : IModel
    {
        /*
        //private static DbProviderFactory _providerFactory = new SqlClientFactory();
        //private DbConnection _connection; = _providerFactory.CreateConnection();
        private static DbProviderFactory GetFactory()
        {
            // register SqlClientFactory in provider factories
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            return DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
        }
        */

        private static UserAccess _userAccess;

        private SqlConnection       _sqlServerCnn; // = new SqlConnection() ;
        private SqliteConnection    _sqLiteCnn; // = new SqliteConnection() ;

        private DataBaseType  _dbType;
        private IModel      _dataObject;
        private string      _dbTableName;

        private DatabaseTransaction _isTransaction = DatabaseTransaction.Yes;
        private DatabaseConnection _dbConnectionType = DatabaseConnection.Local;
        private DbTransaction _transaction;
        
        #region Constructors
        private DAODB() { }

        private DAODB(ref UserAccess userAccess) 
        {
            _userAccess = userAccess;
            this._dbType = _userAccess.DDBBType;
            if (this._dbConnectionType == DatabaseConnection.Local)
            {
                string cnnstr = new AppDDBBcfg(_userAccess.DDBBType).GetConnectionString;
                switch (this._dbType)
                {
                    case DataBaseType.SqLite:
                        this._sqLiteCnn = new SqliteConnection(cnnstr);
                        break;
                    case DataBaseType.SqlServer:
                        this._sqlServerCnn = new SqlConnection(cnnstr);
                        break;
                }
            }
        }

        internal DAODB(ref UserAccess userAccess, T dataObject, 
                    DatabaseTransaction transaction = DatabaseTransaction.Yes, 
                    DatabaseConnection connectionType = DatabaseConnection.Local) 
                    : this(ref userAccess)
        {
            this._dataObject = dataObject;
            this._isTransaction = transaction;
            this._dbTableName = dataObject.IGetTableName();
            if (connectionType == DatabaseConnection.Remote)
                throw new Exception("DAODB: Remote connection -> Method not implemented");
        }

        #endregion

        #region Properties (Insert, Update, Delete, Connection, ConnectionString)

        private SqlConnection Sqlconnection
        {
            get
            {
                if (this._sqlServerCnn.State != ConnectionState.Open)
                    this._sqlServerCnn.Open();
                return this._sqlServerCnn;
            }
        }

        private SqliteConnection SqLiteconnection
        {
            get
            {
                if (this._sqLiteCnn.State != ConnectionState.Open)
                    this._sqLiteCnn.Open();
                return this._sqLiteCnn;
            }
        }

        private DbConnection Connection
        {
            get 
            {
                switch (_userAccess.DDBBType)
                {
                    case DataBaseType.SqLite: return this.SqLiteconnection;
                    case DataBaseType.SqlServer: return this.Sqlconnection;
                    default: return null;
                }
            }
        }

        private bool CloseConnection
        {
            get 
            {
                if (this._sqLiteCnn != null)
                {
                    if (this._sqLiteCnn.State == ConnectionState.Closed)
                        this._sqLiteCnn.Close();
                }
                if (this._sqlServerCnn != null)
                {
                    if (this._sqlServerCnn.State != ConnectionState.Closed)
                        this._sqlServerCnn.Close();
                }
                return true; 
            }
        }

        internal int Insert
        {
            get { return this.execNonQuery(this.createInsertCommand()); }
        }

        internal int Update
        {
            get { throw new Exception("Update Method not implemented."); }
        }

        internal int DeleteById
        {
            get { return this.execNonQuery(this.createDeleteCommandById()); }
        }

        #endregion

        #region Create command parameters

        private DbParameter createCommandParameter(ref DbCommand command, string paramName, object paramValue) //Type paramType, 
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = "@" + paramName;
            param.Value = paramValue;
            //param.DbType = (DbType)Enum.Parse(typeof(DbType), paramType.Name);
            return param;
        }

        #endregion

        #region SQL -> Select function : Return Lists of elements.

        internal List<T> getData(DataFilter filter)
        {
            List<T> data = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            DbCommand _cmd = this.createSelectCommand(ref filter);
            _cmd.Connection = this.Connection;
            
            DbDataReader reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(typeof(T));
                    foreach (PropertyInfo pi in properties)
                    {
                        //if(pi.CustomAttributes.Count() == 0)
                        try
                        {
                            pi.SetValue(obj, reader[pi.Name], null);
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        
                    }
                    data.Add(obj);
                }
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                bool ok = this.CloseConnection;
            }

            return data;
        }

        private DbCommand createSelectCommand(ref DataFilter filter)
        {
            DbCommand command = this.Connection.CreateCommand();

            string sqlJoin = this._dataObject.IGetSQLJoin();
            string sql = sqlJoin.Contains(" JOIN ") ? sqlJoin : "SELECT * FROM [" + this._dbTableName + "] ";
            string sqlWhere = "";
            
            if (filter.GetFilter.Count > -1)
            {
                foreach (DFilter f in filter.GetFilter)
                {
                    if (f.Clausule !="" && f.FieldName != "")
                    {
                        command.Parameters.Add(this.createCommandParameter(ref command, f.FieldName, f.Value));
                        sqlWhere += f.Clausule + " " + f.FieldName + f.Syntax + "@"+ f.FieldName;
                    }
                }
            }
            command.CommandText = sql + sqlWhere + (_userAccess.DDBBType == DataBaseType.SqLite ? ";" : "");
            return command;
        }

        #endregion

        #region SQL -> Insert function
        private DbCommand createInsertCommand()
        {
            DbCommand command = this.Connection.CreateCommand();
            StringCollection scFields = new StringCollection();
            StringCollection scValues = new StringCollection();
            object propertyValue;
            DAOAttributes daoAttribute;

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                propertyValue = pi.GetValue(this._dataObject, null);
                daoAttribute = (DAOAttributes)Attribute.GetCustomAttribute(pi, typeof(DAOAttributes));

                if (pi.Name.ToUpper() != "ID" && daoAttribute == null)
                {
                    scFields.Add(new StringBuilder().AppendFormat("{0}", pi.Name).ToString());
                    scValues.Add(new StringBuilder().AppendFormat("@{0}", pi.Name).ToString());
                    command.Parameters.Add(this.createCommandParameter(ref command, pi.Name, propertyValue));
                }
            }
            command.CommandText = this.generateSQL(DataActions.Insert, scFields, scValues);
            return command;
        }

        #endregion

        #region SQL -> Delete function 
        private DbCommand createDeleteCommandById()
        {
            DbCommand command = this.Connection.CreateCommand();
            StringCollection scFields = new StringCollection();

            long id = this._dataObject.IGetId();
            if (id > 0)
            {
                scFields.Add(new StringBuilder().AppendFormat("{0} = @{0}", "ID").ToString());
                command.Parameters.Add(createCommandParameter(ref command, "ID", id));
                command.CommandText = this.generateSQL(DataActions.Delete, scFields, new StringCollection());
            }
            return command;
        }

        #endregion

        //TODO: exclude DAOJoin Att from update. Also create the method
        #region SQL -> Update function


        #endregion

        #region SQL -> Generate SQL statement

        private string generateSQL(DataActions dbAction, StringCollection Fields, StringCollection Values)
        {
            string sql = string.Empty;

            string[] arrFields = new string[Fields.Count];
            Fields.CopyTo(arrFields, 0);

            string[] arrValues = new string[Values.Count];
            Values.CopyTo(arrValues, 0);

            switch (dbAction)
            {
                case DataActions.Insert:
                    sql = "INSERT INTO " + this._dbTableName +
                            "(" + string.Join(", ", arrFields) + ") VALUES (" + string.Join(", ", arrValues) + ");";
                    break;
                case DataActions.Update:
                    sql = "UPDATE " + this._dbTableName +
                            " SET " + string.Join(", ", arrFields) +
                            " WHERE " + string.Join(" AND ", arrValues) + ";";
                    break;
                case DataActions.Delete:
                    sql = "DELETE FROM " + this._dbTableName +
                                            " WHERE " + string.Join(" AND ", arrFields) + ";";
                    break;
            }

            return sql;
        }

        #endregion

        // TODO: return last id (not working with this version of sql)
        #region Execute NonQuery

        private int execNonQuery(DbCommand _cmd)
        {
            int lid = 0;

            try
            {
                if (_cmd.Parameters.Count > 0)
                {
                    //_cmd = _providerFactory.CreateCommand();
                    _cmd.Connection = this.Connection;

                    if (this._isTransaction == DatabaseTransaction.Yes)
                    {
                        //this._transaction = this._connection.BeginTransaction();
                        this._transaction = this.Connection.BeginTransaction();
                        _cmd.Transaction = this._transaction;

                        lid = _cmd.ExecuteNonQuery();

                        //_cmd.CommandText = "SELECT SCOPE_IDENTITY(); ";
                        //lid = Convert.ToInt32(_cmd.ExecuteScalar());
                        this._transaction.Commit();
                    }
                    else
                        lid = _cmd.ExecuteNonQuery();
                }
                else
                {
                    throw new ArgumentNullException("Command has no parameters.");
                }
            }
            catch (Exception ex)
            {
                if (this._isTransaction == DatabaseTransaction.Yes)
                    this._transaction.Rollback();

                throw new Exception("SQL: " + _cmd.CommandText + " " + ex.Message);
            }
            finally
            {
                this.Connection.Close();
                //bool ok = this.CloseConnection;
            }
            return lid;
        }

        #endregion
    
    }
}
