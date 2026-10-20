using System;

namespace App.Definitions.Entity
{
    public sealed class UserLoginInput
    {
        private readonly string _userName;
        private readonly string _password;

        #region Constructors
        public UserLoginInput(string user, string pass) 
        {
            if(string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                throw new ArgumentException("Incorrect parameters");

            this._userName = user;
            this._password = pass;
        }

        #endregion

        #region Properties
        public string User
        { 
            get { return this._userName; }
            private set { }
        }

        public string Password
        { 
            get { return this._password; }
            private set { }
        }

        #endregion

    }
}
