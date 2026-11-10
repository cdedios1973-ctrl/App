using System;
using System.Collections.Generic;

namespace ERP.Domain.Interfaces.View;

public interface ILoginView
{
    // Propiedades para leer los TextBox
    string Usuario { get; }
    string Password { get; }

    // Eventos que disparará el formulario
    event EventHandler LoginClick;
    event EventHandler CerrarClick;
    event EventHandler MinimizarClick;

    // Acciones que el Presenter ordenará a la Vista
    void MostrarErrores(List<string> errores);
    void CerrarConExito();
    void MinimizarVentana();
    void CerrarAplicacion();
    void Login();
}
