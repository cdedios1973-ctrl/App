namespace App.Config.DDBB
{
    using System.Data;
    using Microsoft.Data.SqlClient;
    using App.Definitions.Enums;

    internal class AppSQLServerCreate : AppInitialize
    {
        internal AppSQLServerCreate() { }

        #region Sql Server : Delete database related files.

        internal bool SQLServerDeleteFiles(string databaseFile)
        {
            try
            {
                if (File.Exists(databaseFile))
                {
                    File.Delete(databaseFile);
                    File.Delete(databaseFile.Replace(".mdf", ".ldf"));
                }
                return true;
            }
            catch
            {
                throw new Exception(DELETE_FILE_ERROR + _dbName);
            }
        }

        #endregion

        #region SQL Server : Drop Database

        internal bool SQLServerDrop()
        {
            _appDDBBConfig.MasterDatabase = SqlServerMaster.Yes;
            using (SqlConnection conn = new SqlConnection(_appDDBBConfig.GetConnectionString))
            {
                try
                {
                    conn.Open();
                    string sqlCommandText = @"ALTER DATABASE " + _dbName +
                        @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + _dbName + "]";
                    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, conn);
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Error " + DROPPING_DDBB + " " + _dbName);
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        #endregion

        #region Sql Server : Create Database Properties

        internal bool SQLServerCreate(string fileName, SqlServerMaster master)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection();

            _appDDBBConfig.MasterDatabase = master;
            conn.ConnectionString   = _appDDBBConfig.GetConnectionString;
            string databaseProps    = _appDBSchema.GetDatabaseProps(fileName);

            SqlCommand cmd = new SqlCommand(databaseProps, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                ok = true;
            }
            catch
            {
                throw new Exception(CREATE_DDBB_ERROR + _dbName);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ok;
        }

        #endregion

        #region Sql Server : Create tables and populate it

        internal bool SQLServerCreateTables()
        {
            bool ok = true;
            try
            {
                List<(string, string)> schemas = _appDBSchema.GetTableSchemas(DataBaseType.SqlServer);
                List<(string, string)> tableData = _appDBSchema.GetTableData();

                setInitData(schemas, "Creating");
                setInitData(tableData, "Populating");
            }
            catch (Exception ex)
            {
                ok = false;
                this.manageException(_dbName + " error: " + ex.Message);
            }
            return ok;
        }

        private void setInitData(List<(string, string)> data, string title)
        {
            if (data.Count > 0)
            {
                SqlCommand SQLServercmd;
                _appDDBBConfig.MasterDatabase = SqlServerMaster.No;
                using (SqlConnection SQLServerconn = new SqlConnection(_appDDBBConfig.GetConnectionString))
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
        }

        #endregion

    }
}
