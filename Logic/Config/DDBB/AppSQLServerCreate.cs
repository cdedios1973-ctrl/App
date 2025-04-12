namespace App.Config.DDBB
{
    using App.Enums;
    using App.Logic;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Data;

    internal class AppSQLServerCreate
    {

        private AppSQLServerCreate() { }

        internal AppSQLServerCreate(string fileName, SqlServerMaster master)
        {

        }

        internal bool initSQLServerDDBB(string fileName, SqlServerMaster master)
        {
            bool ok = false;
            /*
            SqlConnection conn = new SqlConnection();
            _appDDBBConfig.MasterDatabase = master;
            conn.ConnectionString = _appDDBBConfig.GetConnectionString;

            messageHandler.updateStatus("Creating Database...", "in process");

            SqlCommand cmd = new SqlCommand(setDatabaseProps(fileName), conn);
            try
            {
                messageHandler.updateStatus("Opening connection to Database...", "in process");

                conn.Open();
                cmd.ExecuteNonQuery();
                ok = true;
                messageHandler.updateStatus("Creating " + _userAccess.DDBBType.ToString(), "done");
            }
            catch (Exception ex)
            {
                messageHandler.updateStatus(_userAccess.DDBBType.ToString() + ": " + ex.Message, "Error");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            */
            return ok;
        }

    }
}
