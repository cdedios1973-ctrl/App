using System;

using App.View.Models;

/// <summary>
/// Layer responsible for communicating view requests to logic calls
/// </summary>

namespace App.Controller.Login
{
    public class LoginController //: IController
    {

        public LoginController(UserLoginInputDTO user) 
        {
            if(user == null) 
                throw new ArgumentNullException("User and pass must be filled");
        }



        // TODO : Call LoginService interface from AppLogic.
        // TODO : Need to implement ILoginService just because its a different layer (decouples DB/Logic).

        // The view calls LoginController directly passing a filled DTO.
        // Reasons:
        // a) Controller acts as the entry point for the view into the system.
        // b) DTO travels freely as a pure data contract between layers.

    }
}
