using System;

using ERP.Domain.Auth;
using ERP.Domain.DTO;
using ERP.Domain.Interfaces.Login;
using ERP.Application.Interfaces.Persistence;

// DONE : En vez de devolver user, devolver bool -> solo necesita saber si el user esta logado.
// DONE : Para simplicación puedo usar la clase UserLoginQuery de la tabla para la validacion de entrada del usuario.
// TODO : Implementar seguridad de microsoft.
// TODO : Instanciar el LoginService desde controlador pasándole ILoginService.

namespace App.Logic.Security
{
    internal sealed class LoginService : ILoginService
    {
        private readonly IAuthUserReadRepository _userLoginRepository;
        
        #region Constructors.

        internal LoginService(IAuthUserReadRepository userToLogin)
        {
            this._userLoginRepository = userToLogin;
        }

        #endregion

        #region Login -> Repository

        public bool Login(UserLoginQueryDto userToLogin)
        {
            // TODO : Cuando aplique la seguridad de microsoft, usar .IsInRole()
            // y manejar aqui la logica para almacenarlos en memoria.
            AuthUserEntity user = this._userLoginRepository.Find(userToLogin);

            return user.IsAuthenticated;

        }

        #endregion

    }
}
