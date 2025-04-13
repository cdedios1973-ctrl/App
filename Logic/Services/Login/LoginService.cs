using System;
using App.Definitions.Enums;
using App.Definitions.Filters;
using App.Models.DTO;
using App.Logic.Models.POCO;

using App.Infraestructure.DAO;

namespace App.Logic.Security
{

    // TODO : IP block, attemps, other security options
    // TODO : Dirrefent layers. (Microsoft security, or for pure boredoom testing, make my own
    internal sealed class LoginService
    {
        private const string INPUT_FORBIDDEN = "User not found.";

        private string _userName;
        private string _password;
        private UserAccess _userToLogin;

        #region Constructors.

        internal LoginService(UserLoginInput input, UserAccess userToLogin)
        {
            this._userName = input.User;
            this._password = input.Password;
            this._userToLogin = userToLogin;
        }

        #endregion

        #region Login -> DAO
        internal bool LoginAction()
        {
            UserDTO user;
            List<UsersPermission> permissions;

            try
            {
                // TODO : Filter class redoo

                DataFilterList filter = new DataFilterList(SQLClausule.WHERE, "UserName", "=", this._userName);
                filter.AddFilter(" AND ", "Password", "=", this._password);


                // TODO : redoo DAO to send class with data.
                // call could be List<UserLogin> users = new DAOData<UserLogin>(this._userToLogin).GetData;

                // TODO : Remove generic at DAO

                List<UserLogin> users = new DAOData<UserLogin>(this._userToLogin, filter).GetData;

                if (users.Count == 1)
                {
                    filter.Clear();
                    filter.AddFilter(SQLClausule.WHERE, "id", "=", users[0].UserId.ToString());
                    user = new DAOData<User>(this._userToLogin, filter).GetData[0];

                    filter.Clear();
                    filter.AddFilter(SQLClausule.WHERE, "userId", "=", users[0].UserId.ToString());
                    permissions = new DAOData<UsersPermission>(this._userToLogin, filter).GetData;

                    // TODO : modify userascces (by ref)
                    //this._userAccess = new UserAccess(ref user, ref permissions);
                }
                else if (users.Count > 1)
                {
                    throw new Exception("DAO Login: Check UserLogin table, integrity fail.");
                }
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message); 
            }

            return this._userToLogin.IsUserAuthenticated;
        }

        #endregion
    
    }
}
