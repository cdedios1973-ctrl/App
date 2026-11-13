using System;

/// <summary>
/// POCO de la tabla maestra 'UserLogin' (Logic - Persistence).
/// </summary>

namespace ERP.Domain.Entity;

internal sealed record class UserLogin
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="UserLogin"/>.
    /// </summary>
    /// <param name="id">UserLogin.Id (PK)</param>
    /// <param name="userId">UserLogin.UserId (FK Unique)</param>
    /// <param name="username">UserLogin.UserName</param>
    /// <param name="password">UserLogin.Password</param>
    public UserLogin(long id, long userId, string username, string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = username;
        this.Password = password;
    }
}
