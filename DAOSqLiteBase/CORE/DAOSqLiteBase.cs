using System;
using System.Data;
using Microsoft.Data.Sqlite;

using App.Infraestructure.CORE.Interfaces.Database;
using App.Infraestructure.CORE.Interfaces.Reader;

// TODO : This class is a tool..
// TODO : IConnectionStringService remove, only need to read once (to avoid going to app.config and do the process everytime)
// SEARCH BETTER OPTION : Can modify Constructor to receive a List of commands and use the foreach at the transaction.
// TODO : I had solved this before, Bill and Bill<Detail> in the same transaction.
//        Bill and Billines are linked by BillNumber ... so after saving Bill, search for BillNumber to fill FK_BILLNUMBER, also FK_BILL_ID.
// TODO : get last id (if proceed for relational tables, if it use FK_ID_RELATED_TABLE)
// TODO : Register at log at catch (SqliteException ex)
// UPGRADE : use 'usings'.

/// <summary>
/// what this class does.......
/// </summary>
/// <param name="example">what this does.</param>
/// <returns>what this class/method return.</returns>

namespace App.Infraestructure.Services.DAO.SqLite
{
    internal sealed class DAOSqLiteBase : IDbExecutor
    {
        private const string INCORRECT_PARAMS = "DAOSqLiteBase: Incorrect parameters: ";
        private readonly IConnectionStringService _connectionString;
        private SqliteCommand _command;

        #region Constructors

        internal DAOSqLiteBase(SqliteCommand commands, IConnectionStringService connectionString)
        {
            if (commands == null)
                throw new ArgumentNullException(INCORRECT_PARAMS, nameof(commands));

            if (connectionString == null)
                throw new ArgumentNullException(INCORRECT_PARAMS, nameof(connectionString));

            this._command = commands;
            this._connectionString = connectionString;
        }

        #endregion

        #region Interface Implementation

        public long ExecuteCrud()
        {
            int lid = 0;
            string msg = string.Empty;
            bool flagCommit = false;

            SqliteTransaction transaction = null;
            SqliteConnection connection = null;

            try
            {
                // TODO : ConnectionString must be know before hand (static class)
                connection = new SqliteConnection(_connectionString.GetConnectionString()); 

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                transaction = connection.BeginTransaction();
                this._command.Connection = connection; 
                this._command.Transaction = transaction;
                lid = this._command.ExecuteNonQuery();
                flagCommit = true;

                transaction.Commit();

            }
            catch (SqliteException ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    msg = "Rollback Exception Type: " + ex2.GetType();
                }
                msg += ex.Message;
            }
            finally 
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();

                if (msg != string.Empty)
                    throw new Exception(msg);
            }
            return lid;
        }

        #endregion
    
    }
}