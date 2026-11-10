using System;
using ERP.Domain.DTO;
using ERP.Domain.Auth;
using ERP.Domain.Interfaces.Login;
using ERP.Application.Interfaces.Persistence;

// DONE : En vez de devolver user, devolver bool -> solo necesita saber si el user esta logado.
// DONE : Para simplicación puedo usar la clase UserLoginQuery de la tabla para la validacion de entrada del usuario.
// DONE : Instanciar el LoginService desde controlador/presenter pasándole ILoginService.
// TODO : Implementar seguridad de microsoft. (Comprobacion que esta en red local o active directory)

namespace ERP.Application.Services.Security;

internal sealed class LoginService : ILoginService
{
    private readonly IAuthUserReadRepository _userLoginRepository;
    
    internal LoginService(IAuthUserReadRepository userToLogin)
    {
        this._userLoginRepository = userToLogin;
    }

    public bool Login(UserLoginQueryDto userToLogin)
    {
        if (userToLogin == null || string.IsNullOrEmpty(userToLogin.UserName) || string.IsNullOrEmpty(userToLogin.Password))
            throw new ArgumentException("Datos incorrectos para realizar el login.");

        AuthUserEntity? user = this._userLoginRepository.Find(userToLogin);

        // TODO: Guardar el AuthUserEntity clase estática de sesión en memoria

        return user.IsAuthenticated;

    }
}
