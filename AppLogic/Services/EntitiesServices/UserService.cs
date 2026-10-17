using System;

using App.View.Models;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Infraestructure.Services.DAO.SqLite;
using App.Logic.Security;
//using Microsoft.IdentityModel.Tokens;

// TODO : Quitar checkuserpermission del constructor y useraccess.
// TODO : instanciar el UserService desde controlador pasándole IConnectionStringService

namespace App.Logic.Services.Entities
{
    internal class UserService
    {
        private const string INCORRECT_PARAM = "Incorrect params.";
        // 1. Declaramos el servicio como un campo de la clase
        private readonly IConnectionStringService _connectionStringService;
        private readonly User _user;

        // 2. Lo pedimos por el constructor para que quien cree el UserService se lo pase
        internal UserService(UserAccess user, UserDTO dto, IConnectionStringService connectionStringService)
        {
            // TODO : prueba de permisos, quitarlo
            if (user.CheckUserPermission("UserService") <= 0)
                throw new UnauthorizedAccessException("User has no permissions to this module.");
            //_token = user; // Token temporal para pruebas.

            if (dto == null || string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException(nameof(dto), INCORRECT_PARAM);

            if (connectionStringService == null)
                throw new ArgumentNullException(nameof(connectionStringService));

            this._user = new User(dto.Id, dto.Name, dto.Surname, dto.Address, dto.CountryIdentity);
            this._connectionStringService = connectionStringService; // Asignamos la cadena
        }

        public bool Register()
        {
            string cnnStr = this._connectionStringService.GetConnectionString();

            // 1. Abres el ejecutor una sola vez
            using (var db = new DAOSqLiteBase(cnnStr))
            {
                // 2. Creas el comando mapeador con los datos del usuario
                var userCommand = new UserCommandSqliteDAO(this._user);

                // 3. Lo ejecutas pasándole el motor
                bool exito = userCommand.Insert(db);
            }

            return true;
        }
    }
}
