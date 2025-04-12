namespace App.DAO
{
    using System;
    using App.Models;
    using App.Enums;
    using App.Logic;
    using App.Logic.IO.Handler;

    // TODO: IP block, attemps, other security options
    public sealed class DAOLogin
    {
        private const string INPUT_FORBIDDEN = "User not found.";

        private string _userName = "";
        private string _password = "";
        private DataBaseType _dbType;
        private DisplaType _display;

        #region Constructors.

        private DAOLogin() { }

        //public DAOLogin(string userName, string password, DataBaseType DataBaseType) 
        public DAOLogin(string input, DataBaseType dbType, DisplaType display) 
        {
            // TODO: IP block, attemps, other security options
            try
            {
                if (InputString.CheckInyection(ref input) == false)
                    throw new Exception(INPUT_FORBIDDEN);

                string[] data = input.Split(',');
                this._userName = data[0];
                this._password = data[1];
                this._dbType = dbType;
                this._display = display;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Properties

        public UserAccess GetValidateUser
        {
            get { return loginAction(); }
        }

        #endregion

        #region Methods / functions for Login.

        private UserAccess loginAction()
        {
            Users user;
            UserAccess userAccess = new UserAccess(this._dbType, this._display);
            List<UsersPermission> permissions;

            try
            {
                DataFilter filter = new DataFilter(" WHERE ", "UserName", "=", this._userName);
                filter.AddFilter(" AND ", "Password", "=", this._password);
                List<UsersLogin> users = new DAOData<UsersLogin>(userAccess, ref filter).GetData;

                if (users.Count == 1)
                {
                    filter.Clear();
                    filter.AddFilter(" WHERE ", "id", "=", users[0].UserId.ToString());
                    user = new DAOData<Users>(userAccess, ref filter).GetData[0];

                    filter.Clear();
                    filter.AddFilter(" WHERE ", "userId", "=", users[0].UserId.ToString());
                    permissions = new DAOData<UsersPermission>(userAccess, ref filter).GetData;
                    userAccess = new UserAccess(ref user, ref permissions);
                }
                else if (users.Count > 1)
                    throw new Exception("DAO Login: Check UserLogin table, integrity fail.");
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }

            return userAccess;
        }

        #endregion
    
    }
}
