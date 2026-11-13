using System;
//using System.Text.Json.Serialization;

namespace ERP.Application.DTO;

/// <summary>
/// DTO de la tabla maestra 'UserLogin' (Vista).
/// Implementa una relación 1:1 para desacoplar las credenciales de acceso de los datos personales del usuario.
/// </summary>
/// <remarks>
/// ÁREAS DE CONTROL Y ARQUITECTURA:
/// - Restricciones de BD: Clave única en UserId (UserId Unique, UserName Unique, MachineName Unique, Password).
/// - Ámbito: Diseñado inicialmente para entornos de Intranet.
/// - Seguridad: Autenticación básica. Pendiente de migrar a un sistema basado en Roles/Tokens (Identity).
/// - Escalabilidad: Permite el particionamiento o separación física de la BD de accesos en el futuro.
/// </remarks>

//[Serializable()] // TODO: Cambiar a [JsonSerializable] cuando se migren los serializadores
public sealed record class UserLoginDto
{
    public long Id { get; init; }

    public long UserId { get; init; }

    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserLoginDto"/>.
    /// </summary>
    /// <param name="id">UserLogin.Id (PK)</param>
    /// <param name="userId">UserLogin.UserId (FK)</param>
    /// <param name="username">UserLogin.UserName</param>
    /// <param name="password">UserLogin.Password</param>

    public UserLoginDto(long id, long userId, string username, string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = username;
        this.Password = password;
    }
}
