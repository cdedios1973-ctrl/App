using System;
using Microsoft.IdentityModel.Tokens;

using App.Models;
using App.Definitions.Entity;
using App.Definitions.Interfaces.DAOCommands;
using App.Logic.Security;
using App.Definitions.Interfaces;

// TODO : Necesito aplicar la "seguridad" antes de instanciar a esta clase,
// TODO : Quitar checkuserpermission del constructor y useraccess.
// TODO : Microsoft.IdentityModel.Tokens => (estudiar)


namespace App.Logic.Services.Entities
{
    internal class UserService
    {
        private readonly IUserCommandsDAO _userCommandsDao;
        private ITokenCredential _token;

        internal UserService(UserAccess user, IUserCommandsDAO userCommandsDao)
        {

            // 🧪 NOTA DE LABORATORIO PERSONAL:
            // Sé que acoplar 'UserAccess' aquí rompe SOLID (Inversión de Dependencias) y POO (depender de clases concretas).
            // En un entorno profesional, esta validación y el manejo de tokens se delegarían 
            // al framework nativo mediante un Middleware, Claims y un IHttpContextAccessor (Inyección de Dependencias) (falta por aprender).
            if (user.CheckUserPermission("UserService") <= 0)
                throw new UnauthorizedAccessException("User has no permissions to this module.");

            _token = user; // temporal token, just for testing pourposes.
            _userCommandsDao = userCommandsDao;
        }

        public long Register(UserDTO dto)
        {
            User user = new User(dto.Id, dto.Name, dto.Surname, dto.Address, dto.CountryIdentity);

            long lid = _userCommandsDao.Insert(user, _token);

            return lid;
        }
    }
}
