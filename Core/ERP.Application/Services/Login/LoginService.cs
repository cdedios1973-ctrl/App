using System;
using ERP.Application.Security;
using ERP.Application.Interfaces.LoginService;
using ERP.Application.Interfaces.Persistence;
using ERP.Domain.Queries;

namespace ERP.Application.Services;

// 1. Ejemplo de Excepción Personalizada de Negocio
public class BusinessRuleException : Exception
{
    // Se Puede pasar una lista de errores o un texto acumulado
    public BusinessRuleException(string message) : base(message) { }
}

public class LoginService : ILoginService
{
    private readonly IAuthUserReadRepository _authRepository;

    public LoginService(IAuthUserReadRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public void Execute(UserLoginQueryDto user)
    {
        // --- SECCIÓN FAIL-FAST ---
        // Acumular posibles errores antes de tocar la base de datos
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(user.UserName))
            errors.Add("El campo 'Usuario' es obligatorio.");

        if (string.IsNullOrWhiteSpace(user.Password))
            errors.Add("El campo 'Contraseña' es obligatorio.");

        // Si falla algo, Custom Exception
        if (errors.Any())
        {
            string msg = string.Join(Environment.NewLine, errors);
            throw new BusinessRuleException(msg);
        }

        var result = _authRepository.Find(user);

        if (result == null || !PasswordHasher.Verify(user.Password, result.PasswordHash))
            throw new BusinessRuleException("El usuario o la contraseña no son válidos.");

        //TODO : crear sesion del usuario (aqui ya puedo crear un objeto de sesion sin el PasswordHash).

    }
}
