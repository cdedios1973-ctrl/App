//using Microsoft.IdentityModel.Tokens;

using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.EntityService;
using App.Infrastructure.Persistence;
//using Microsoft.Data.Sqlite;

// TODO : Implementar en App.Start() -> Inicializacion de herramientas.
// TODO : IConnectionStringService -> en app.start -> leer solo una vez (actualmente no esta muy bien optimizado para leer constantemente)
// TODO : Implementar seguridad de microsoft.
// TODO : Lo hare en persitencia : Antes de insertar Comprobar que no existe el usuario (NIF/CIF o identificador único), aunque debe estar protegido por la constrain, pero se evita el salto en el autonumerico

namespace App.Logic.Services.Entities
{
    internal sealed class UserService : IUserService
    {
        private const string INCORRECT_PARAM = "Parámetros incorrectos.";
        private readonly IUserWriteRepository _userRepository;
        private readonly IConnectionStringService _connectionStringService;

        #region Constructor

        public UserService(IUserWriteRepository userReposity, IConnectionStringService connectionStringService)
        {
            if (connectionStringService == null)
                throw new ArgumentNullException(nameof(connectionStringService));

            if (userReposity == null)
                throw new ArgumentNullException(nameof(userReposity));

            _userRepository = userReposity;
            _connectionStringService = connectionStringService;
        }

        #endregion


        // TODO : Volver a desacoplar a Infraestructure // la logica solo procesa aqui validaciones y tranformacion dt->poco

        #region Interface Implementation Insert Method

        public bool InsertUser(UserDto user)
        {
            // 1. Validaciones defensivas de entrada
            if (user == null)
                throw new ArgumentException(nameof(user), INCORRECT_PARAM);

            // 2. Mapeo de datos a entidad de dominio
            // Si hubiese que realizar calculos seria aqui.
            User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);

            return this._userRepository.Insert(userEntity);

        }

        #endregion

        // TODO : Implementar Update en Repository.
        #region Interface Implementation Update Method

        public bool UpdateUser(UserDto user)
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

                long lid = this._userRepository.Update(userEntity, conn, trans);

                if (lid > 0)
                {
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

        // TODO : Implementar Delete en Repository.
        #region Interface Implementation Delete Method

        public bool DeleteUser(UserDto user)
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

                isSuccess = this._userRepository.Delete(userEntity, conn, trans);

                if (isSuccess)
                    trans.Commit();
                else
                    trans.Rollback();
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