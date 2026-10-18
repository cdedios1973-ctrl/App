//using Microsoft.IdentityModel.Tokens;

using App.Definitions.DTO;
using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Definitions.Interfaces.Entity;
using App.Infraestructure.CORE.Interfaces.DAOCommands;

// TODO : instanciar el UserService desde controlador pasándole IConnectionStringService
// TODO : Implementar Update y Delete

namespace App.Logic.Services.Entities
{
    internal class UserService : IUserService
    {
        private const string INCORRECT_PARAM = "Parámetros incorrectos.";
        private readonly IUserCommandDAO _userDAO;
        private readonly IConnectionStringService _connectionStringService;
        private User _user;

        public UserService(IUserCommandDAO userDAO, IConnectionStringService connectionStringService)
        {
            _userDAO = userDAO;
            _connectionStringService = connectionStringService;
        }

        public bool AddNewUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentException(nameof(user), INCORRECT_PARAM);

                // Convertimos el DTO a la entidad de negocio (POCO)
                var userEntity = new User(user.Id, user.Name, user.Surname, user.Address, user.CountryIdentity);
                // El DAO se encarga por dentro de abrir la conexión, la transacción y hacer el Rollback si falla.
                return _userDAO.InsertWithTransaction(userEntity);
        }
    }
}
