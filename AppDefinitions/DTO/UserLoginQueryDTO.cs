using System;

namespace App.Definitions.DTO
{
    public sealed class UserLoginQueryDTO
    {
        private readonly string _userName;
        private readonly string _password;

        #region Constructors
        internal UserLoginQueryDTO(string userName, string passsWord) 
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
        { get { return this._userName; } }

        public string Password
        { get { return this._password; } }

        #endregion

    }
}
