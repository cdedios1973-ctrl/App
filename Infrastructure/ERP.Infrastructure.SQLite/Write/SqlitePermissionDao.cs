using System;

// Deprecated -> Movido a infrastructure

/*
//using Microsoft.IdentityModel.Tokens;

using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).
// TODO : Arreglar la documentacion
// TODO : Hash de campos.

namespace App.Infrastructure.Persistence
{
    public class SqlitePermissionDao : IPermissionWriteRepository //, IToken
    {
        private const string SQL_INSERT = "INSERT INTO zzzzzz (zzzzz, zzzzzz) VALUES (@zzzz, @zzzzz); SELECT last_insert_rowid();";
        // TODO: Implementar constante sql de modificación y eliminacion
        //private const string SQL_UPDATE = "";
        //private const string SQL_DELETE = "";

        #region Constructor
        public SqlitePermissionDao() { }

        #endregion

        #region Interface Implementation

        public long Insert(Permission permissions, SqliteConnection connection, SqliteTransaction transaction)
        {
            if (permissions == null)
                throw new ArgumentNullException("permissions");

            long lid = 0;
            SqliteCommand cmd = null;

            try
            {
                cmd = new SqliteCommand(SQL_INSERT, connection, transaction);
                cmd.Parameters.AddWithValue("@zzzz", permissions.Name);
                //cmd.Parameters.AddWithValue("@zzzz", permissions.Surname);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    lid = Convert.ToInt64(result);
            }
            catch (SqliteException)
            {
                throw;
            }
            finally
            {
                if (cmd != null)
                    ((IDisposable)cmd).Dispose();
            }
            return lid;
        }

        public long Update(Permission permissions, SqliteConnection connection, SqliteTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Permission permissions, SqliteConnection connection, SqliteTransaction transaction)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
*/