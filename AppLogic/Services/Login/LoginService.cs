using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.Login;
using App.Infrastructure.Persistence;
using System;



// DONE : En vez de devolver user, devolver bool -> solo necesita saber si el user esta logado.

// TODO : Implementar seguridad de microsoft.
// TODO : Instanciar el LoginService desde controlador pasándole ILoginService.
// TODO : Recordar que IConnectionStringService se inicializara en el inicio de la aplicacion
// UPGRADE : Para simplicación puedo usar la clase UserLoginQuery de la tabla para la validacion de entrada del usuario.

namespace App.Logic.Security
{
    internal sealed class LoginService : ILoginService
    {
        private readonly IUserLoginReadRepository _userLoginRepository;
        private readonly IConnectionStringService _connectionStringService;
        
        #region Constructors.

        internal LoginService(IUserLoginReadRepository userToLogin, IConnectionStringService connectionStringService)
        {
            this._userLoginRepository = userToLogin;
            this._connectionStringService = connectionStringService;
        }

        #endregion

        #region Login -> DAO

        public bool Login(UserLoginQueryDTO userToLogin)
        {
            // TODO : Guardar el User.Id o User.Name
            // TODO : Aplicar logica adicional si procede.
            User user = _userLoginRepository.LoginAction(userToLogin, _connectionStringService.GetConnectionString());

            return user.Id > 0;
        }
        #endregion
    
    }
}
