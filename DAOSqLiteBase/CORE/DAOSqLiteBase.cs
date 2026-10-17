using App.Infraestructure.CORE.Interfaces.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

/// <summary>
/// what this class does.......
/// </summary>
/// <param name="example">what this does.</param>
/// <returns>what this class/method return.</returns>

// TODO : confianza entre ensamblado para ponerlo en Internal

namespace App.Infraestructure.Services.DAO.SqLite
{
    public sealed class DAOSqLiteBase : IDbExecutor //, IDisposable
    {
        private const string INCORRECT_PARAMS = "DAOSqLiteBase: Incorrect parameters: ";
        private readonly string _connectionString;
        private bool _disposed = false;

        public DAOSqLiteBase(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(INCORRECT_PARAMS, nameof(connectionString));

            this._connectionString = connectionString;
        }

        public bool ExecNonQuery(SqliteCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));

            string msg = string.Empty;
            SqliteTransaction transaction = null;
            SqliteConnection connection = null;

            try
            {
                connection = new SqliteConnection(this._connectionString);
                connection.Open();

                transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SqliteException ex)
            {
                try { transaction?.Rollback(); }
                catch (Exception ex2) { msg = "Rollback Exception Type: " + ex2.GetType() + " "; }
                msg += ex.Message;
                return false;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();

                if (msg != string.Empty)
                    throw new Exception(msg);
            }

            //return string.IsNullOrEmpty(msg) ? true : false;
        }

        public void Dispose()
        {
            // En SQLite clásico no solemos necesitar lógica customizada aquí 
            // ya que abrimos y cerramos la conexión por comando de forma segura.
        }
    }
}
