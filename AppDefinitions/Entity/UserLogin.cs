using System;

namespace App.Definitions.Entity
{
    public sealed class UserLogin
    {
        private readonly long _id = 0;
        private readonly long _userId = 0;
        private readonly string _username;
        private readonly string _password;

        #region Constructors
        public UserLogin(long id, long userId, string username, string password)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User Id is required.");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("User Name must be filled");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password must be filled");
            }

            this._id = id;
            this._userId = userId;
            this._username = username;
            this._password = password;
        }

        #endregion

        #region Properties
        public long Id
        {
            get { return this._id; }
            private set { }
        }
        public long UserId
        {
            get { return this._userId; }
            private set { }
        }
        public string UserName
        {
            get { return this._username; }
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
