using System;

namespace App.View.Models
{
    //[Serializable()]
    public sealed class UserLoginInputDTO
    {
        private readonly string _userName;
        private readonly string _password;

        // TODO : prevous version, need to change to match controller input
        // TODO : Apply service IPreventStringInyection

        #region Constructors
        internal UserLoginInputDTO(string userName, string passsWord) 
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User Name must be filled");
            }
            if (string.IsNullOrWhiteSpace(passsWord))
            {
                throw new ArgumentException("Password must be filled");
            }

            this._userName = userName;
            this._password = passsWord;
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
