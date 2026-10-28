using System;

/// <summary>
/// Sesion de Usuario; uso interno de la aplicacion; Auditoria de registros.
///<remarks>Esta clase NO es serializabe.</remarks> 
/// </summary>

namespace App.Definitions.Auth;

public sealed record class AuthUserEntity
{
    private long UserId { get; init; }
    private string UserName { get; init; }
    public bool IsAuthenticated
    {
        get { return this.UserId > 0 && !string.IsNullOrEmpty(this.UserName); }
    }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="AuthUserEntity"/>.
    /// </summary>
    /// <param name="userId">Id de usuario</param>
    /// <param name="userName">Nombre de usuario</param>
    public AuthUserEntity(long userId, string userName)
    {
        this.UserId = userId;
        this.UserName = userName;
    }
}
