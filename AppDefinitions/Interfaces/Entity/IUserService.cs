using System;

using App.Definitions.DTO;

namespace App.Definitions.Interfaces.Entity
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
        /// <returns>El DTO del usuario registrado con su identificador generado.</returns>
        /// <exception cref="ArgumentException">Se lanza si los datos de entrada no son válidos.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error inesperado en el procesamiento.</exception>
        bool AddNewUser(UserDTO user);

    }

}
