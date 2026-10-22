using System;

using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.Login;

//using App.Definitions.DTO;
//using App.Definitions.Enums;
//using App.Infraestructure.CORE.Interfaces.DAOCommands;
//using App.Infraestructure.DAO;
//using App.Logic.Models.POCO;
//using App.Models.DTO;
//using AppDefinitions.Deprecated.DataFilters;

// TODO : Implementar seguridad de microsoft.
// TODO : instanciar el LoginService desde controlador pasándole ILoginService.
// TODO : Recordar que IConnectionStringService se inicializara en el inicio de la aplicacion
namespace App.Logic.Security
{
    internal sealed class LoginService : ILoginService
    {
        //private const string INPUT_FORBIDDEN = "User not found.";
        //private string _userName;
        //private string _password;
        //private UserAccess _userToLogin;
        //private const string INCORRECT_PARAM = "Parámetros incorrectos.";

        private readonly ILoginDao _loginDao;
        private readonly IConnectionStringService _connectionStringService;
        
        #region Constructors.

        internal LoginService(ILoginDao userToLogin, IConnectionStringService connectionStringService)
        {

            this._loginDao = userToLogin;
            this._connectionStringService = connectionStringService;
            //this._userName = input.User;
            //this._password = input.Password;
            //this._userToLogin = userToLogin;
        }

        #endregion

        #region Login -> DAO

        public User Login(UserLogin userToLogin) // <- Implementacion de interfaz
        {
            return _loginDao.LoginAction(userToLogin, _connectionStringService);
        }


        // Deprecated
        /*
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
        */

        #endregion
    
    }
}
