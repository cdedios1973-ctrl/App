using System;
using Microsoft.Data.Sqlite;
using ERP.Domain.Auth;
using ERP.Domain.DTO;
using ERP.Domain.Interfaces.Config;
using ERP.Application.Interfaces.Persistence;

namespace ERP.Infrastructure.Persistence;

internal class SqliteAuthUserRepository : IAuthUserReadRepository
{

    private readonly IConnectionStringService _connectionStringService;

    #region Constructor

    public SqliteAuthUserRepository(IConnectionStringService connectionStringService)
    {
        this._connectionStringService = connectionStringService;
    }

    #endregion

    #region Interface Implementation

    public AuthUserEntity Find(UserLoginQueryDto userToLogin)
    {
        if (userToLogin == null)
            throw new ArgumentNullException(nameof(userToLogin));

        const string SQL_SELECT = "SELECT [User].[Id], [User].[Name] " +
        "FROM UserLogin INNER JOIN USER ON [UserLogin].[Id] = [User].[ID] " +
        "WHERE [UserLogin].[UserName] = @UserName AND [UserLogin].[Password] = @Password;";

        AuthUserEntity loggedUser = null;

        using var connection = new SqliteConnection(this._connectionStringService.GetConnectionString());
        using var cmd = new SqliteCommand(SQL_SELECT, connection);

        cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userToLogin.UserName;
        cmd.Parameters.Add("@Password", SqliteType.Text).Value = userToLogin.Password;

        connection.Open();

        using SqliteDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            loggedUser = new AuthUserEntity(Convert.ToInt32(reader["Id"]), reader["Name"].ToString());

            if (reader.Read())
                throw new InvalidOperationException("¡Alerta crítica! Se detectaron múltiples usuarios con las mismas credenciales.");
        }

        // Al llegar al final del método, C# hará Dispose en orden inverso automático: 
        // 1º reader, 2º cmd, 3º connection. ¡Impecable y sin anidar llaves!


        /*
        using (var connection = new SqliteConnection(this._connectionStringService.GetConnectionString()))
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
         */
        return loggedUser;
    }

    #endregion

}