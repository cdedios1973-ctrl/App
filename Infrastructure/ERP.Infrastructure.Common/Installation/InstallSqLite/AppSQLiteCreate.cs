using System;
using System.Data;

//using App.Enums;

//using Microsoft.Data.Sqlite;

namespace App.Infraestructure.Installation.Database
{ 
    /// <summary>
    /// Refactoring poo+solid+cc. 
    /// a) Service to delete file. (Remove this option before production)
    /// b) Service / class to initialize Demo Database
    /// c) Service / class to create relational tables into Database
    /// </summary>

    // TODO : Convert this class to Sqlite db installer.

    internal class AppSQLiteCreate //: AppInitialize
    {
        internal AppSQLiteCreate() { }

        // TODO : delete this, search db at app/db folder manually and delete it by hand.
        /*
        #region SqLite : Delete database related files.

        internal bool SQLiteDeleteFiles(string databaseFile)
        {
            try
            {
                if (File.Exists(databaseFile))
                    File.Delete(databaseFile);
                return true;
            }
            catch
            {
                throw new Exception(DELETE_FILE_ERROR + _dbName);
            }
        }

        #endregion
        */


        // TODO : Create service to create database only if doesn't exist (already got service to find file). filename is static AppDb
        #region SQLite : Create Database

        internal bool SQLiteCreate(string fileName)
        {
            bool ok = true;
            SqliteConnection conn = new SqliteConnection();
            try
            {
                File.WriteAllBytes(fileName, new byte[0]);
                conn.ConnectionString = _appDDBBConfig.GetConnectionString; // already got service for cnnstring...
                conn.Open();
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();

                if(ok == false)
                    throw new Exception(CREATE_DDBB_ERROR + " AppDb"); // create const
            }
            return ok;
        }

        #endregion


        // TODO : Create service / class to create tables.
        #region SQLite : Create tables and populate it

        internal bool SQLiteCreateTables()
        {
            bool ok = true;
            try
            {
                List<(string, string)> schemas = _appDBSchema.GetTableSchemas(DataBaseType.SqLite);
                List<(string, string)> tableData = _appDBSchema.GetTableData();

                setInitData(schemas, "Creating");
                setInitData(tableData, "Populating");
            }
            catch (Exception ex)
            {
                ok = false;
                throw new Exception(_dbName + " error: " + ex.Message);
            }
            return ok;
        }

        private void setInitData(List<(string, string)> data, string title)
        {
            if (data.Count > 0)
            {
                SqliteCommand SqLitecmd;
                using (SqliteConnection SqLiteconn = new SqliteConnection(_appDDBBConfig.GetConnectionString))
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
        }

        #endregion

    }
}
