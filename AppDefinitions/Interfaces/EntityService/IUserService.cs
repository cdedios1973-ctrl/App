using System;

using App.Definitions.DTO;

namespace App.Definitions.Interfaces.EntityService
{
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
        bool AddUser(UserDTO user);

    }

}
