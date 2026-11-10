using System;

using ERP.Domain.DTO;
using ERP.Domain.Interfaces.Login;
using ERP.Domain.Interfaces.View;

/// <summary>
/// Layer responsible for communicating view requests to logic calls
/// </summary>

namespace ERP.Presenter.Login;

internal sealed class LoginPresenter
{
    private readonly ILoginView _view;
    private readonly ILoginService _loginService;

    public LoginPresenter(ILoginView view, ILoginService loginservice) 
    { 
        this._view = view;
        this._loginService = loginservice;

        _view.LoginClick += OnLoginClick;
        _view.CerrarClick += (s, e) => _view.CerrarAplicacion();
        _view.MinimizarClick += (s, e) => _view.MinimizarVentana();
    }

    private void OnLoginClick(object? sender, EventArgs e)
    {
        var dto = new UserLoginQueryDto(_view.Usuario, _view.Password);
        try
        {
            if(this._loginService.Login(dto))
                this._view.CerrarConExito();
        }
        catch (Exception ex)
        {
            _view.MostrarErrores(new List<string> { "Error inesperado: " + ex.Message });
        }
    }
}
