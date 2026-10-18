using System;

using App.View.Models;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Infraestructure.Services.DAO.SqLite;
using App.Logic.Security;
//using Microsoft.IdentityModel.Tokens;

// TODO : instanciar el UserService desde controlador pasándole IConnectionStringService

namespace App.Logic.Services.Entities
{
    internal class UserService_bk
    {
        private const string INCORRECT_PARAM = "Incorrect params.";
        private readonly IConnectionStringService _connectionStringService;
        private readonly User _user;

        internal UserService_bk(UserAccess user, UserDTO dto, IConnectionStringService connectionStringService)
        {
            // TODO : prueba de permisos, quitarlo despues de ejecutar aplicacion
            if (user.CheckUserPermission("UserService") <= 0)
                throw new UnauthorizedAccessException("User has no permissions to this module.");

            if (dto == null || string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException(nameof(dto), INCORRECT_PARAM);

            if (connectionStringService == null)
                throw new ArgumentNullException(nameof(connectionStringService));

            this._user = new User(dto.Id, dto.Name, dto.Surname, dto.Address, dto.CountryIdentity);
            this._connectionStringService = connectionStringService;
        }

        public bool Register()
        {
            bool ok = false;
            string cnnStr = this._connectionStringService.GetConnectionString();

            using (var db = new DAOSqLiteBase(cnnStr))
            {
                var userCommand = new UserCommandSqliteDAO(this._user);
                ok = userCommand.Insert(db);
            }

            return ok;
        }
    }
}
