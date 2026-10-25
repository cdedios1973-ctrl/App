using System;
using Microsoft.Data.Sqlite;
using App.Definitions.DTO;
using App.Definitions.Auth;
using App.Definitions.Predicates; // No creo que me haga falta
using TaskHub.Infrastructure.Paths;

namespace App.Infrastructure.Persistence
{
    internal class SqliteAuthUserRepository : IAuthUserReadRepository
    {
        private const string SQL_SELECT = "SELECT [User].[Id], [User].[Name] " +
            "FROM UserLogin INNER JOIN USER ON [UserLogin].[Id] = [User].[ID] " +
            "WHERE [UserLogin].[UserName] = @UserName AND [UserLogin].[Password] = @Password;";

        private readonly IPathProvider _pathProvider;
        private readonly string _connectionString;


        #region Constructor
        public SqliteAuthUserRepository(IPathProvider _provider)
        {
            if( _provider == null ) 
                throw new ArgumentNullException(nameof( _provider));


            PathInfo info = this._pathProvider.GetAppGlobalPaths().Find(PathInfoFilters.WithKey("db"));


            this._pathProvider = _provider;
            //this._connectionString = info.
        }

        #endregion

        #region Interface Implementation

        public AuthUserEntity Find(UserLoginQueryDTO userToLogin)
        {
            if (userToLogin == null)
                throw new ArgumentNullException(nameof(userToLogin));

            AuthUserEntity loggedUser = null;

            using (var connection = new SqliteConnection(this._connectionString))
            {
                using (var cmd = new SqliteCommand(SQL_SELECT, connection))
                {
                    cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userToLogin.UserName;
                    cmd.Parameters.Add("@Password", SqliteType.Text).Value = userToLogin.Password;

                    connection.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loggedUser = new AuthUserEntity(Convert.ToInt32(reader["Id"]), reader["Name"].ToString());

                            if (reader.Read())
                                throw new InvalidOperationException("¡Alerta crítica! Se detectaron múltiples usuarios con las mismas credenciales. Integridad de DB comprometida.");
                        }
                    }
                }
            }

            return loggedUser;
        }

        #endregion

    }
}