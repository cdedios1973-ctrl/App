using System;
using Microsoft.Data.Sqlite;
using ERP.Domain.Interfaces.Config;
using ERP.Domain.Auth;
using ERP.Application.Interfaces.Persistence;

namespace ERP.Infrastructure.Sqlite;

internal class SqliteAuthUserRepository : IAuthUserReadRepository
{
    private readonly IConnectionStringService _connectionStringService;

    public SqliteAuthUserRepository(IConnectionStringService connectionStringService)
    {
        this._connectionStringService = connectionStringService;
    }

    // public AuthUserEntity Find(UserLoginQueryDto userToLogin)
    public UserAuthQueryDto Find(string userName, string password) // (usando tipos primitivos en el parámetro):
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException("SqliteAuthUserRepository: Incorrect arguments");

        const string SQL_SELECT = "SELECT [User].[Id], [User].[Name] " +
        "FROM UserLogin INNER JOIN USER ON [UserLogin].[Id] = [User].[ID] " +
        "WHERE [UserLogin].[UserName] = @UserName AND [UserLogin].[Password] = @Password;";

        UserAuthQueryDto loggedUser = null;

        using var connection = new SqliteConnection(this._connectionStringService.GetConnectionString());
        using var cmd = new SqliteCommand(SQL_SELECT, connection);

        // cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userToLogin.UserName;
        // cmd.Parameters.Add("@Password", SqliteType.Text).Value = userToLogin.Password;
        cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userName;
        cmd.Parameters.Add("@Password", SqliteType.Text).Value = password;

        connection.Open();

        using SqliteDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // loggedUser = new AuthUserEntity(Convert.ToInt32(reader["Id"]), reader["Name"].ToString());
            // Nuevo Read Model
            loggedUser = new UserAuthQueryDto(Convert.ToInt32(reader["Id"]), reader["Name"].ToString());

            if (reader.Read())
                throw new InvalidOperationException("¡Alerta crítica! Se detectaron múltiples usuarios con las mismas credenciales.");
        }

        return loggedUser;
    }
}