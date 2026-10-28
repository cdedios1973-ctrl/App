using System;

/// <summary>
/// DTO de petición para comprobar la existencia de un usuario a través de su nombre de usuario y clave.
/// </summary>

namespace App.Definitions.DTO;

public sealed record class UserLoginQueryDto
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserLoginQueryDto"/>.
    /// </summary>
    /// <param name="userName">UserLogin.UserName</param>
    /// <param name="password">UserLogin.Password</param>
    public UserLoginQueryDto(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }
}