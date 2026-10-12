using System;
using App.Logic.IO.Handler;
using App.Logic.Models.POCO;
using App.Logic.Security;

namespace App.Logic.Services.Commands
{
    public sealed class AppCommandLogin
    {
        private const string MESSAGE_SENDER = "AppCommandLogin";
        private const string LOGGED_FAIL    = "Incorrect User or Password.";

        private UserAccess _user;

        public AppCommandLogin(UserAccess user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            this._user = user;
        }

        public bool LogIn(string input) 
        {
            if (CheckInput.Injection(input) == true)
            {
                return false;
            }

            // Empty UserAccess (minumal permissions to log in) -> Filled UserAccess -> I have the modules applicattion permissions

            var loginService = new LoginService(new UserLoginInput(input), this._user).LoginAction() ;

            if (this._user.UserId <= 0)
            {
                throw new UnauthorizedAccessException($"{MESSAGE_SENDER} : {LOGGED_FAIL}");
            }

            return this._user.UserId > 0;
        }
    }
}
