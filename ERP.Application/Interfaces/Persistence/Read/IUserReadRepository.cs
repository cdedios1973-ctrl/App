using System;
using System.Collections.Generic;
using ERP.Application.DTO;

namespace ERP.Application.Interfaces.Persistence;

/// <summary>
/// Esta interfaz define la accion básica para leer datos de la tabla User en la base de datos.
/// </summary>
public interface IUserReadRepository
{
    /// <summary>
    /// Lee registro de usuario. Requiere nivel de permiso 1 o superior.
    /// </summary>
    /// <param name="user">El filtro.</param>
    /// <returns>Devuelve una lista de usuarios (dto).</returns>
    List<UserDto> UserQueryRepository(UserDto user);

}