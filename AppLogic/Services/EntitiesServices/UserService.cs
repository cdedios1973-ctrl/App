//using Microsoft.IdentityModel.Tokens;

using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.Entity;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using Microsoft.Data.Sqlite;

// TODO : instanciar el UserService desde controlador pasándole IConnectionStringService
// TODO : Implementar Update y Delete

namespace App.Logic.Services.Entities
{
    internal class UserService : IUserService
    {
        private const string INCORRECT_PARAM = "Parámetros incorrectos.";
        private readonly IUserDao _userDAO;
        private readonly IConnectionStringService _connectionStringService;
        private User _user;

        public UserService(IUserDao userDAO, IConnectionStringService connectionStringService)
        {
            _userDAO = userDAO;
            _connectionStringService = connectionStringService;
        }

        public bool AddNewUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentException("user", INCORRECT_PARAM);

            User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);

            SqliteConnection conn = null;
            SqliteTransaction trans = null;
            bool isSuccess = false;

            try
            {
                conn = new SqliteConnection(_connectionStringService.GetConnectionString());
                conn.Open();
                trans = conn.BeginTransaction();

                long newId = _userDAO.Insert(userEntity, conn, trans);

                if (newId > 0)
                {
                    // [MODULARIDAD DE LAS FK]
                    // _permissionDAO.InsertUserPermissions(newId, user.Permissions, conn, trans);
                    trans.Commit();
                    isSuccess = true;
                }
                else
                    trans.Rollback();
            }
            catch (Exception)
            {
                if (trans != null)
                    trans.Rollback();

                throw; // Reenviamos el error para que el Controller lo gestione de forma desacoplada
            }
            finally
            {
                // Limpieza manual y estricta de recursos de red en .NET 2.0
                if (trans != null)
                    ((IDisposable)trans).Dispose();

                if (conn != null)
                {
                    conn.Close();
                    ((IDisposable)conn).Dispose();
                }
            }

            return isSuccess;
        }
    }
}