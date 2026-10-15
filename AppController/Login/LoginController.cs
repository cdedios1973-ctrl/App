using System;

/// Summary : Layer responsible for communicating view requests to logic calls

namespace App.Controller.Login
{
    public class LoginController
    {
        // TODO : Call LoginService interface from AppLogic.
        // TODO : Need to implement ILoginService just because its a different layer (decouples DB/Logic).

        // The view calls LoginController directly passing a filled DTO.
        // Reasons:
        // a) Controller acts as the entry point for the view into the system.
        // b) DTO travels freely as a pure data contract between layers.

    }
}
