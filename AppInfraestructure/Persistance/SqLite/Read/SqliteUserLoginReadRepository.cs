using System;
using Microsoft.Data.Sqlite;
using App.Definitions.DTO;
using App.Definitions.Entity.Auth;

namespace App.Infrastructure.Persistence
{
    public class SqliteUserAuthRepository //: Ilo
    {
        private const string SQL_SELECT = "SELECT [User].[Id], [User].[Name] " +
            "FROM UserLogin INNER JOIN USER ON [UserLogin].[Id] = [User].[ID] " +
            "WHERE [UserLogin].[UserName] = @UserName AND [UserLogin].[Password] = @Password;";

        #region Constructor
        public SqliteUserAuthRepository() { }

        #endregion

        #region Interface Implementation

        public AuthUserEntity Select(UserLoginQueryDTO userToLogin, SqliteConnection connection)
        {
            if (userToLogin == null)
                throw new ArgumentNullException(nameof(userToLogin));

            SqliteCommand cmd = null;
            AuthUserEntity loggedUser = null;

            try
            {
                cmd = new SqliteCommand(SQL_SELECT, connection);
                cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userToLogin.UserName;
                cmd.Parameters.Add("@Password", SqliteType.Text).Value = userToLogin.Password;

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        if (count > 1)
                            throw new InvalidOperationException("Error crítico de seguridad: Se encontró más de un usuario con las mismas credenciales.");

                        loggedUser = new AuthUserEntity(reader["Id"], reader["Name"]);
                    }
                }
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

            return loggedUser;
        }

        #endregion
    
    }
}