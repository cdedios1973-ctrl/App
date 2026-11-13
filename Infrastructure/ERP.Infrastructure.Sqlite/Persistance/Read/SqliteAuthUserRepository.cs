using ERP.Application.DTO.Auth;
using ERP.Application.Interfaces.Persistence;
using ERP.Domain.Interfaces.Config;
using ERP.Domain.Queries;
using Microsoft.Data.Sqlite;
using System;
using System.Xml.Linq;

namespace ERP.Infrastructure.Sqlite;

internal class SqliteAuthUserRepository : IAuthUserReadRepository
{
    private readonly IConnectionStringService _connectionStringService;

    public SqliteAuthUserRepository(IConnectionStringService connectionStringService)
    {
        this._connectionStringService = connectionStringService;
    }

    public AuthUserDto? Find(UserLoginQueryDto userToLogin)
    {
        if (userToLogin == null || string.IsNullOrEmpty(userToLogin.UserName))
            throw new ArgumentNullException(nameof(userToLogin), "SqliteAuthUserRepository: Incorrect arguments");

        const string SQL_SELECT = "SELECT [User].[Id], [User].[Name], [User].[Password], [User].[MachineName] " +
        "FROM UserLogin INNER JOIN USER ON [UserLogin].[UserId] = [User].[ID] " +
        "WHERE [UserLogin].[UserName] = @UserName;";

        AuthUserDto? loggedUser = null;

        using var connection = new SqliteConnection(this._connectionStringService.GetConnectionString());
        using var cmd = new SqliteCommand(SQL_SELECT, connection);

        cmd.Parameters.Add("@UserName", SqliteType.Text).Value = userToLogin.UserName;

        connection.Open();

        using SqliteDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            int idIndex = reader.GetOrdinal("Id");
            int nameIndex = reader.GetOrdinal("Name");
            int passIndex = reader.GetOrdinal("Password");
            int machineIndex = reader.GetOrdinal("MachineName");

            loggedUser = new AuthUserDto(
                userId: reader.GetInt64(idIndex),
                userName: reader.GetString(nameIndex),
                passwordHash: reader.GetString(passIndex),
                machineName: reader.GetString(machineIndex));

            if (reader.Read())
                throw new InvalidOperationException("¡Alerta crítica! Se detectaron múltiples usuarios con las mismas credenciales.");
        }

        return loggedUser;
    }
}