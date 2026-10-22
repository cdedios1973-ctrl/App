using System;
//using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

// UPGRADE : Token permission, permission needed >= 0 (Search microsoft documentation security roles).
// TODO : Arreglar la documentacion
// TODO : Hash de campos.

namespace App.Infrastructure.Persistence
{
    public class SqliteUserLoginDao //: Ilo //, IToken
    {
        private const string SQL_SELECT = "INSERT INTO zzzzzz (zzzzz, zzzzzz) VALUES (@zzzz, @zzzzz); SELECT last_insert_rowid();";

        #region Constructor
        public SqliteUserLoginDao() { }

        #endregion

        #region Interface Implementation

        public User Login(UserLogin userLogin, SqliteConnection connection, SqliteTransaction transaction)
        {
            if (userLogin == null)
                throw new ArgumentNullException("userLogin");

            //long lid = 0;
            SqliteCommand cmd = null;

            // buscar usuario por user/pass

            /*

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
            */
            return lid;

        }

        #endregion
    
    }
}