using System;
using ERP.Domain.DTO;

namespace ERP.Domain.Interfaces.EntityService;

/// <summary>
/// Define las operaciones de lógica de negocio para la gestión de usuarios.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registra un nuevo usuario en el sistema tras validar sus datos.
    /// </summary>
    /// <param name="user">Objeto DTO con los datos del usuario a crear.</param>
    /// <returns>Devuelve verdadero si la operación es satisfactoria, falso en caso contrario.</returns>
    bool InsertUser(UserDto user);

    /// <summary>
    /// Modifica un usuario en el sistema tras validar sus datos.
    /// </summary>
    /// <param name="user">Objeto DTO con los datos del usuario a modificar.</param>
    /// <returns>Devuelve verdadero si la operación es satisfactoria, falso en caso contrario.</returns>
    bool UpdateUser(UserDto user);

    /// <summary>
    /// Elimina un usuario en el sistema tras validar sus datos.
    /// </summary>
    /// <param name="user">Objeto DTO con los datos del usuario a eliminar.</param>
    /// <returns>Devuelve verdadero si la operación es satisfactoria, falso en caso contrario.</returns>
    bool DeleteUser(UserDto user);
}
