using ERP.Domain.DTO;
using ERP.Domain.Interfaces.Controller;
using ERP.Domain.Interfaces.EntityService;
using ERP.Domain.Interfaces.Login;
using System;

/// <summary>
/// Layer responsible for communicating view requests to logic calls
/// </summary>

namespace ERP.Controller.Login;

internal sealed class LoginController : ILoginController
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginservice) 
    { 
        this._loginService = loginservice;
    }

    public List<string> Login(UserLoginQueryDto dto)
    {
        var errors = new List<string>();

        // 1. Validaciones en memoria (Fail-Fast de negocio)
        if (dto == null)
        {
            errors.Add("Los datos usuario y password no pueden ser nulos.");
            return errors;
        }
        try
        {
            errors = this._loginService.Login(dto);
        }
        catch (Exception ex)
        {
            errors.Add("Operacion de guardado incorrecto: " + ex.Message);
        }
        return errors;
    }
}
