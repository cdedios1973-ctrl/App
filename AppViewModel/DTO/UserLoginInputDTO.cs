using System;

namespace App.View.Models
{
    [Serializable()]
    internal sealed class UserLoginInputDTO
    {
        private readonly string _userName;
        private readonly string _password;

        #region Constructors
        internal UserLoginInputDTO(ref string inputdata) 
        {
            if(!inputdata.Contains(","))
                throw new ArgumentException("Incorrect parameters");

            string[] data = inputdata.Split(',');
            this._userName = data[0];
            this._password = data[1];
        }

        #endregion

        #region Properties
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
