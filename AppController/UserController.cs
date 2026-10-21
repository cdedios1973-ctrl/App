using System;
using App.Definitions.DTO;
using App.Definitions.Interfaces.EntityService;

// Controlador básico, SIN SEGURIDAD, para aplicaciones pequeñas a nivel de red local / intranet, con comentarios de uso y guia.
namespace App.UI.Presenters
{
    public class UserFormPresenter
    {
        private readonly IUserService _userService;

        // Inyección limpia de dependencias (Fail-Fast de infraestructura)
        public UserFormPresenter(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Procesa el registro acumulando estados en una lista.
        /// </summary>
        public List<string> ExecuteRegister(UserDTO userRawData)
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
    }
}