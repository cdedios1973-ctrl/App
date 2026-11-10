using System;

using ERP.Application.DTO;
using ERP.Application.Interfaces.EntityService;

// Controlador básico, SIN SEGURIDAD de momento, para aplicaciones pequeñas a nivel de red local / intranet, con comentarios de uso y guia.
namespace ERP.UI.Presenters;

public class UserFormPresenter
{
    private readonly IUserService _userService;

    #region Constructor

    // Inyección limpia de dependencias (Fail-Fast de infraestructura)
    public UserFormPresenter(IUserService userService)
    {
        if(userService == null)
            throw new ArgumentNullException(nameof(userService));

        this._userService = userService;
    }

    #endregion

    // TODO : Mover la validacion a la logica, en la transformacion de dto a poco deberia saltar el error
    // TODO : Ademas es alli donde hay que aplicar la seguridad de Microsoft para que no instancie a Persistencia si no tiene permisos
    // TODO : Tambien creo que se aplica mejor alli SOLID, donde se valida la Logica
    /// <summary>
    /// Procesa el registro acumulando estados en una lista.
    /// </summary>
    #region Logic Service Call { Add New User }

    public List<string> ExecuteRegister(UserDto userRawData)
    {
        var errors = new List<string>();

        // 1. Validaciones en memoria (Fail-Fast de negocio)
        if (userRawData == null)
        {
            errors.Add("Los datos del usuario no pueden ser nulos.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(userRawData.Name))
            errors.Add("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(userRawData.Surname))
            errors.Add("El apellido es obligatorio.");

        // Si hay errores de entrada, frena antes de tocar la red local
        if (errors.Count > 0)
            return errors;

        // 2. Persistencia protegida contra fallos físicos de la intranet
        try
        {
            bool success = _userService.InsertUser(userRawData);

            if (success)
                errors.Add("Operacion de guardado correcto.");
            else
                errors.Add("Operacion de guardado incorrecto: El servidor rechazó el registro.");
        }
        catch (Exception ex)
        {
            errors.Add("Operacion de guardado incorrecto: " + ex.Message);
        }
        return errors;
    }

    #endregion

}