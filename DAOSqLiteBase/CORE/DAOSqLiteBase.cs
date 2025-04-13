using System;
using System.Data;
using Microsoft.Data.Sqlite;

using App.Infraestructure.CORE.Interfaces.Database;
using App.Infraestructure.CORE.Interfaces.Reader;

namespace App.Infraestructure.Services.DAO.SqLite
{
    internal sealed class DAOSqLiteBase : IDbExecutor
    {
        private const string INCORRECT_PARAMS = "DAOSqLiteBase: Incorrect parameters: ";
        private readonly IConnectionStringService _connectionString;
        private SqliteCommand _command;

        // TODO : Can modify Constructor to receive a List of commands and use the foreach at the transaction.
        #region Constructors

        internal DAOSqLiteBase(SqliteCommand command, IConnectionStringService connectionString)
        {
            if (command == null)
                throw new ArgumentNullException(INCORRECT_PARAMS, nameof(command));

            if (connectionString == null)
                throw new ArgumentNullException(INCORRECT_PARAMS, nameof(connectionString));

            this._command = command;
            this._connectionString = connectionString;
        }

        #endregion

        // TODO : I can use 'usings' and delegate the responsability. I prefer to refresh me how was done before.
        // TODO : get last id (if proceed for relational tables, if it use FK_ID_RELATED_TABLE)
        // TODO : Register at log at catch (SqliteException ex)
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
                connection = new SqliteConnection(_connectionString.GetConnectionString());

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                this._command.Connection = connection; 

                transaction = connection.BeginTransaction();

                this._command.Transaction = transaction;

                lid = this._command.ExecuteNonQuery();

                flagCommit = true;

                transaction.Commit();

            }
            catch (SqliteException ex)
            {
                if (flagCommit == false)
                    transaction.Rollback();

                msg = ex.Message;
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