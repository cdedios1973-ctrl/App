using System;

using App.Definitions.Auth;
using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.Login;
using App.Infrastructure.Persistence;

// DONE : En vez de devolver user, devolver bool -> solo necesita saber si el user esta logado.
// DONE : Para simplicación puedo usar la clase UserLoginQuery de la tabla para la validacion de entrada del usuario.

// TODO : Implementar seguridad de microsoft.
// TODO : Instanciar el LoginService desde controlador pasándole ILoginService.
// TODO : Recordar que IConnectionStringService se inicializara en el inicio de la aplicacion

namespace App.Logic.Security
{
    internal sealed class LoginService : ILoginService
    {
        private readonly IAuthUserReadRepository _userLoginRepository;
        private readonly IConnectionStringService _connectionStringService;
        
        #region Constructors.

        internal LoginService(IAuthUserReadRepository userToLogin, IConnectionStringService connectionStringService)
        {
            this._userLoginRepository = userToLogin;
            this._connectionStringService = connectionStringService;
        }

        #endregion

        #region Login -> Repository

        public bool Login(UserLoginQueryDto userToLogin)
        {
            // TODO : Cuando aplique la seguridad de microsoft, usar .IsInRole()
            // y manejar aqui la logica para almacenarlos en memoria.
            AuthUserEntity user = this._userLoginRepository.Find(userToLogin).UserId > 0;

            return user.UserId > 0;
        }
        #endregion

    }
}
