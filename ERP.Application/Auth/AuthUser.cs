using System;

/// <summary>
/// Sesion de Usuario; uso interno de la aplicacion; Auditoria de registros.
///<remarks>Esta clase NO es serializabe.</remarks> 
/// </summary>

namespace ERP.Application.Auth;

public sealed record class AuthUser
{
    internal long UserId { get; init; }
    internal string UserName { get; init; }
    internal string MachineName { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="AuthUser"/>.
    /// </summary>
    /// <param name="userId">Id de usuario</param>
    /// <param name="userName">Nombre de usuario</param>
    /// <param name="machineName">Identidad del pc</param>
    public AuthUser(long userId, string userName, string machineName)
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId), "El ID de usuario debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentNullException(nameof(userName), "El Nombre de usuario no puede estar vacio.");

        if (string.IsNullOrWhiteSpace(machineName))
            throw new ArgumentNullException(nameof(machineName), "El identificador del equipo (MachineName) es obligatorio para iniciar sesión.");

        UserId = userId;
        UserName = userName;
        MachineName = machineName;
    }
}
