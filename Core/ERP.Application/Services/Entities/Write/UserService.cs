//using Microsoft.IdentityModel.Tokens;
//using App.Definitions.Interfaces.Config;
//using Microsoft.Data.Sqlite;

using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.EntityService;
using App.Infrastructure.Persistence;


// TODO : Aqui es donde debo implementar la seguridad de microsoft, para que no llegue a la capa de persistencia
// TODO : Implementar en main() -> Inicializacion de herramientas.
// TODO : IConnectionStringService -> en main() -> leer solo una vez (actualmente no esta muy bien optimizado para leer constantemente)
// TODO : Antes de insertar Comprobar que no existe el usuario (NIF/CIF o identificador único), aunque debe estar protegido por la constrain, pero se evita el salto en el autonumerico
// DONE : Volver a desacoplar a Infraestructure // la logica solo procesa aqui validaciones y tranformacion dto->poco

namespace App.Logic.Services.Entities
{
    internal sealed class UserService : IUserService
    {
        private const string INCORRECT_PARAM = "Parámetros incorrectos.";
        private readonly IUserWriteRepository _userRepository;

        #region Constructor

        public UserService(IUserWriteRepository userReposity)
        {
            if (userReposity == null)
                throw new ArgumentNullException(nameof(userReposity));

            _userRepository = userReposity;
        }

        #endregion

        #region Interface Implementation { Insert, Update, Delete Methods }

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

        public bool UpdateUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentException(nameof(user), INCORRECT_PARAM);

            // TODO : Comprobar que campos son los obigatorios y cuales no se deben de cambiar, p.e. NIF o identificativo único

            // Mapeo manual a entidad de dominio
            User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);
            return this._userRepository.Update(userEntity);
        }

        public bool DeleteUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentException(nameof(user), INCORRECT_PARAM);

            bool isSuccess = false;

            if (user.Id > 0)
            {
                User userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);
                isSuccess = this._userRepository.Delete(userEntity);
            }
            return isSuccess;
        }

        #endregion

    }
}