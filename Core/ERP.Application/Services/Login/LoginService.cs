using System;
using ERP.Application.Interfaces.Persistence;
using ERP.Domain.Auth;
using ERP.Domain.DTO;
//using ERP.Domain.Entity;
using ERP.Domain.Interfaces.Login;


// DONE : En vez de devolver user, devolver bool -> solo necesita saber si el user esta logado.
// DONE : Para simplicación puedo usar la clase UserLoginQuery de la tabla para la validacion de entrada del usuario.
// TODO : Implementar seguridad de microsoft.
// TODO : Instanciar el LoginService desde controlador pasándole ILoginService.

namespace ERP.Application.Services.Security;

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

    public List<string> Login(UserLoginQueryDto userToLogin)
    {
        // TODO : Cuando aplique la seguridad de microsoft, usar .IsInRole()

        var errors = new List<string>();

        if (userToLogin == null || string.IsNullOrEmpty(userToLogin.UserName) || string.IsNullOrEmpty(userToLogin.Password))
            errors.Add("Datos incorrectos para realizar el login");

        AuthUserEntity? user = this._userLoginRepository.Find(userToLogin);

        if (user == null || !user.IsAuthenticated)
        {
            errors.Add("Usuario o contraseña incorrectos.");
        }
        else
        {
            // TODO: Aquí guardarás el AuthUserEntity en tu clase estática de sesión en memoria
        }
        return errors;

    }

    #endregion
}
