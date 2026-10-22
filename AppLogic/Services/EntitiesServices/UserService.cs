//using Microsoft.IdentityModel.Tokens;

using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.EntityService;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using Microsoft.Data.Sqlite;

// TODO : Implementar Update y Delete en DAO.
// TODO : IConnectionStringService -> en app.start -> leer solo una vez (actualmente no esta muy bien optimizado para leer constantemente)
// TODO : Implementar seguridad de microsoft.
// TODO : Implementar en App.Start() -> Inicializacion de herramientas.

namespace App.Logic.Services.Entities
{
    internal sealed class UserService : IUserService
    {
        private const string INCORRECT_PARAM = "Parámetros incorrectos.";
        private readonly IUserDao _userDAO;
        private readonly IConnectionStringService _connectionStringService;

        #region Constructor

        public UserService(IUserDao userDAO, IConnectionStringService connectionStringService)
        {
            if (connectionStringService == null)
                throw new ArgumentNullException(nameof(connectionStringService));

            if (userDAO == null)
                throw new ArgumentNullException(nameof(userDAO));

            _userDAO = userDAO;
            _connectionStringService = connectionStringService;
        }

        #endregion

        #region Interface Implementation Insert Method

        public bool InsertUser(UserDTO user)
        {
            // 1. Validaciones defensivas de entrada
            if (user == null)
                throw new ArgumentException("user", INCORRECT_PARAM);

            // 2. Declaración de recursos
            SqliteConnection conn = null;
            SqliteTransaction trans = null;
            bool isSuccess = false;

            // 3. Mapeo de datos a entidad de dominio
            User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);

            // 4. Bloque de operaciones críticas 
            try
            {
                // Asignación e inicio de canales de datos
                conn = new SqliteConnection(_connectionStringService.GetConnectionString());
                conn.Open();
                trans = conn.BeginTransaction();

                // Ejecución del comando a través del DAO pasándole el contexto analógico
                long newId = _userDAO.Insert(userEntity, conn, trans);

                // Evaluación del resultado y control de la transacción
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
                // Salvaguarda: Si algo falla, deshacemos cambios inmediatamente
                if (trans != null)
                    trans.Rollback();

                throw; // Redirección del error de forma desacoplada
            }
            finally
            {
                // 5. Limpieza y liberación manual de memoria no administrada
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

        #endregion

        // TODO : Implementar Update en DAO.
        #region Interface Implementation Update Method

        public bool UpdateUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentException("user", INCORRECT_PARAM);

            // Mapeo manual a entidad de dominio
            User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);

            SqliteConnection conn = null;
            SqliteTransaction trans = null;
            bool isSuccess = false;

            try
            {
                conn = new SqliteConnection(_connectionStringService.GetConnectionString());
                conn.Open();
                trans = conn.BeginTransaction();

                int rowsAffected = _userDAO.Update(userEntity, conn, trans);

                if (rowsAffected > 0)
                {
                    trans.Commit();
                    isSuccess = true;
                }
                else
                {
                    trans.Rollback();
                }
            }
            catch (Exception)
            {
                if (trans != null)
                    trans.Rollback();

                throw; // Reenviamos para control desacoplado en capas superiores
            }
            finally
            {
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

        #endregion

        // TODO : Implementar Delete en DAO.
        #region Interface Implementation Delete Method

        public bool DeleteUser(UserDTO user)
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

                int rowsAffected = _userDAO.Delete(userEntity, conn, trans);

                if (rowsAffected > 0)
                {
                    trans.Commit();
                    isSuccess = true;
                }
                else
                {
                    trans.Rollback();
                }
            }
            catch (Exception)
            {
                if (trans != null)
                    trans.Rollback();

                throw;
            }
            finally
            {
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

        #endregion

    }
}