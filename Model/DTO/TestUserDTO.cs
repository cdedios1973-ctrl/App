using System;

namespace App.View.Models
{
    internal sealed class TestUserDTO
    {
        private readonly string _description;
        private readonly string _buttonName;
        private readonly string _userName;
        private readonly string _password;

        #region Constructor
        internal TestUserDTO(string description, string bName, string user, string pass)
        {
            this._description = description;
            this._buttonName = bName;
            this._userName = user;
            this._password = pass;
        }

        #endregion

        #region Properties
        public string Description 
        { 
            get { return this._description; }
            private set { }
        }
        public string ButtonName 
        { 
            get { return this._buttonName;  }
            private set { }
        }
        public string UserName 
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