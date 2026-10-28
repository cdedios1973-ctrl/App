using System;

namespace App.Definitions.DTO;

/// <summary>
/// DTO de petición para comprobar la existencia de un usuario a través de su nombre de usuario y clave.
/// </summary>
public sealed record class UserLoginQueryDto
{

    #region Properties { immutable, locked after creation }

    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserLoginQueryDto"/>.
    /// </summary>
    /// <param name="userName">UserLogin.UserName (Requerido)</param>
    /// <param name="password">UserLogin.Password (Requerido)</param>

    #region Constructors

    public UserLoginQueryDto(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }

    #endregion

}