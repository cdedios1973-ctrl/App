using System;

/// <summary>
/// Sesion de Usuario; uso interno de la aplicacion; Auditoria de registros.
///<remarks>Esta clase NO es serializabe.</remarks> 
/// </summary>

namespace ERP.Domain.Auth;

public sealed record class UserAuthQueryDto
{
    private long UserId { get; init; }
    private string UserName { get; init; }
    public bool IsAuthenticated { get; init; } = false;

    /// <summary>
    /// Inicializa una nueva instancia <see cref="UserAuthQueryDto"/>.
    /// </summary>
    /// <param name="userId">Id de usuario</param>
    /// <param name="userName">Nombre de usuario</param>
    public UserAuthQueryDto(long userId, string userName)
    {
        this.UserId = userId;
        this.UserName = userName;
    }
}
