using System;

/// <summary>
/// Sesion de Usuario; uso interno de la aplicacion; Auditoria de registros.
///<remarks>Esta clase NO es serializabe.</remarks> 
/// </summary>

namespace ERP.Application.DTO.Auth;

public sealed record class AuthUserDto
{
    internal long UserId { get; init; }
    internal string UserName { get; init; }
    internal string PasswordHash { get; init; }
    internal string MachineName { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="AuthUserDto"/>.
    /// </summary>
    /// <param name="userId">Id de usuario</param>
    /// <param name="userName">Nombre de usuario</param>
    /// <param name="passwordHash">Password de usuario</param>
    /// <param name="machineName">Identidad del pc</param>
    public AuthUserDto(long userId, string userName, string passwordHash, string machineName)
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId), "El ID de usuario debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentNullException(nameof(userName), "El Nombre de usuario no puede estar vacio.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException(nameof(passwordHash), "El Password no puede estar vacio.");

        if (string.IsNullOrWhiteSpace(machineName))
            throw new ArgumentNullException(nameof(machineName), "El identificador del equipo (MachineName) es obligatorio para iniciar sesión.");

        UserId = userId;
        UserName = userName;
        PasswordHash = passwordHash;
        MachineName = machineName;
    }
}
