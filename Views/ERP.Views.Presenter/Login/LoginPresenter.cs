using ERP.Application.Interfaces.LoginService;
using ERP.Application.Services;
using ERP.Domain.Queries;
//using ERP.Domain.Queries;
using ERP.View.Presenter.Interfaces;
using System;

/// <summary>
/// Layer responsible for communicating view requests to logic calls
/// </summary>

namespace ERP.View.Presenter.Login;

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
            _loginService.Execute(dto);
            // Ir a la pantalla principal...
        }
        catch (BusinessRuleException ex)
        {
            // Errores controlados de tu lógica (lo que falta, datos inválidos, etc.)
            //_view.MostrarErrores(ex.Message, "Validación"); //, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            // Errores genéricos o técnicos inesperados
            //MessageBox.Show("Ocurrió un error crítico en el sistema.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _view.MostrarErrores(new List<string> { "Error inesperado: " + ex.Message });
        }


    }
}
