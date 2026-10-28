using System;
//using System.Text.Json.Serialization;

namespace App.Definitions.DTO;

/// <summary>
/// DTO de la tabla maestra 'UserLogin' (Vista).
/// Implementa una relación 1:1 para desacoplar las credenciales de acceso de los datos personales del usuario.
/// </summary>
/// <remarks>
/// ÁREAS DE CONTROL Y ARQUITECTURA:
/// - Restricciones de BD: Clave única en UserId y restricción compuesta (UserId, UserName, Password).
/// - Ámbito: Diseñado inicialmente para entornos de Intranet.
/// - Seguridad: Autenticación básica. Pendiente de migrar a un sistema basado en Roles/Tokens (Identity).
/// - Escalabilidad: Permite el particionamiento o separación física de la BD de accesos en el futuro.
/// </remarks>

[Serializable()] // TODO: Estudiar y cambiar a [JsonSerializable] cuando se migren los serializadores

internal sealed record class UserLoginDto
{

    #region Properties { immutable, locked after creation }

    public long Id { get; init; }

    public long UserId { get; init; }

    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserLoginDto"/>.
    /// </summary>
    /// <param name="id">UserLogin.Id (PK)</param>
    /// <param name="userId">UserLogin.UserId (FK)</param>
    /// <param name="username">UserLogin.UserName</param>
    /// <param name="password">UserLogin.Password</param>

    #region Constructor

    public UserLoginDto(long id, long userId, string username, string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = username;
        this.Password = password;
    }

    #endregion

}
