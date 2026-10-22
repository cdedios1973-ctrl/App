using System;
//using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

// TODO : Crear la confianza de emsamblados entre Logica e Infraestructura. para poder poner esta clase internal
// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).
// TODO : Hash de campos. si fuera necesario. repasar.
// TODO : Implementar update y delete.
namespace App.Infrastructure.Persistence
{
    public class SqlitePermissionWriteRepository : IPermissionWriteRepository //, IToken
    {
        private const string SQL_INSERT = "INSERT INTO Permission (Parent, Name, Observation) VALUES (@Parent, @Name, @Observation); SELECT last_insert_rowid();";
        private const string SQL_UPDATE = "UPDATE PERMISSION SET (Parent, Name, Observation) VALUES (@Parent, @Name, @Observation) WHERE ID = @ID;";
        private const string SQL_DELETE = "DELETE FROM PERMISSION WHERE ID = @ID;";

        #region Constructor
        public SqlitePermissionWriteRepository() { }

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
                cmd.Parameters.Add("@Parent", SqliteType.Text).Value = permissions.Parent;
                cmd.Parameters.Add("@Name", SqliteType.Text).Value = permissions.Name;
                cmd.Parameters.Add("@Observation", SqliteType.Text).Value = permissions.Observation;

                object result = cmd.ExecuteScalar();

                if (result != null)
                    lid = (long)result;
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