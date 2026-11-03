using System;
//using Microsoft.IdentityModel.Tokens;

using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using Microsoft.Data.Sqlite;

// DONE : Crear la confianza de emsamblados entre Logica e Infraestructura. para poder poner esta clase internal
// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).
// TODO : Hash de campos. si fuera necesario. repasar.
namespace App.Infrastructure.Persistence
{
    /// <summary>
    /// Proporciona la implementación de comandos SQL de escritura para la entidad <see cref="User"/> en SQLite.
    /// </summary>
    /// <remarks>
    /// Esta clase se enfoca en el uso de tipos primitivos compatibles con el proveedor de datos de SQLite.
    /// </remarks>
    internal class SqliteUserWriteRepository : IUserWriteRepository //, IToken <- Seguridad Microsoft y Auditoria
    {
        //private const string SQL_INSERT = "INSERT INTO User (Name, Surname, Address, CountryIdentity) VALUES (@Name, @Surname, @Address, @CountryIdentity); SELECT last_insert_rowid();";
        //private const string SQL_UPDATE = "UPDATE User SET (Name, Surname, Address, CountryIdentity) VALUES (@Name, @Surname, @Address, @CountryIdentity) WHERE ID = @Id;";
        //private const string SQL_DELETE = "UPDATE User SET (Active) VALUES (@Active) WHERE ID = @Id;";
        //private const string SQL_DELETE = "DELETE FROM User WHERE ID = @Id; ";

        private readonly IConnectionStringService _connectionStringService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SqliteUserWriteRepository"/>.
        /// </summary>
        #region Constructor

        internal SqliteUserWriteRepository(IConnectionStringService connectionStringService)
        {
            this._connectionStringService = connectionStringService;
        }

        #endregion

        #region Interface Implementation { Insert, Update, Delete Actions }

        /// <summary>
        /// Inserta el registro del usuario actual en la base de datos. Modelo previo con bloque try/catch/finally
        /// </summary>
        /// <param name="user">La entidad User a insertar en la base de datos.</param>
        /// <returns><see langword="bool"/>Devuelve verdadero si la transacción ha sido un éxito, falso si ha courrido un error.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Insert(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // 2. Declaración de recursos
            SqliteConnection connection = null;
            SqliteTransaction transaction = null;
            SqliteCommand command = null;

            string SQL_INSERT = "INSERT INTO User (Name, Surname, Address, CountryIdentity) VALUES (@Name, @Surname, @Address, @CountryIdentity); SELECT last_insert_rowid();";
            bool isSuccess = false;
            long lid = 0;

            // 4. Bloque de operaciones críticas 
            try
            {
                // Asignación e inicio de canales de datos
                connection = new SqliteConnection(_connectionStringService.GetConnectionString());
                connection.Open();
                transaction = connection.BeginTransaction();

                command = new SqliteCommand(SQL_INSERT, connection, transaction);
                command.Parameters.Add("@Name", SqliteType.Text).Value = user.Name;
                command.Parameters.Add("@Surname", SqliteType.Text).Value = user.Surname;
                command.Parameters.Add("@Address", SqliteType.Text).Value = user.Address;
                command.Parameters.Add("@CountryIdentity", SqliteType.Text).Value = user.CountryIdentity;

                object result = command.ExecuteScalar();

                if (result != null)
                    lid = (long)result;

                // Evaluación del resultado y control de la transacción
                if (lid > 0)
                {
                    // TODO : Aqui puede ir el bucle foreach con los detalles si procediera
                    // [MODULARIDAD DE LAS FK] (ejemplo)
                    // Mapeo por constructor 1:1 de la clase detalle, enlazada con el ultimo pk o campo combinado
                    transaction.Commit();
                    isSuccess = true;
                }
                else
                    transaction.Rollback();
            }
            catch (SqliteException)
            {
                // Salvaguarda: Si algo falla, deshacemos cambios inmediatamente
                if (transaction != null)
                    transaction.Rollback();

                throw; // Redirección del error de forma desacoplada
            }
            finally
            {
                // 5. Limpieza y liberación manual de memoria no administrada
                if (transaction != null)
                    ((IDisposable)transaction).Dispose();

                if (command != null)
                    ((IDisposable)command).Dispose();

                if (connection != null)
                {
                    connection.Close();
                    ((IDisposable)connection).Dispose();
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// Modifica el registro del usuario actual en la base de datos.
        /// </summary>
        /// <param name="user">La entidad User a modificar en la base de datos.</param>
        /// <returns><see langword="long"/>Devuelve el Id de usuario si la operación es un éxito, 0 si falla.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            string SQL_UPDATE = "UPDATE User SET (Name = @Name, Surname = @Surname, Address = @Address, CountryIdentity = @CountryIdentity) WHERE ID = @Id;";
            bool isSuccess = false;

            try
            {
                // Al declararse con 'using' al inicio, los 3 recursos se liberarán automáticamente EN ORDEN INVERSO al llegar a la llave de cierre del 'try'.
                using var connection = new SqliteConnection(_connectionStringService.GetConnectionString());
                connection.Open();

                using var transaction = connection.BeginTransaction();
                using var command = new SqliteCommand(SQL_UPDATE, connection, transaction);

                command.Parameters.Add("@Id", SqliteType.Integer).Value = user.Id;
                command.Parameters.Add("@Name", SqliteType.Text).Value = user.Name;
                command.Parameters.Add("@Surname", SqliteType.Text).Value = user.Surname;
                command.Parameters.Add("@Address", SqliteType.Text).Value = user.Address;
                command.Parameters.Add("@CountryIdentity", SqliteType.Text).Value = user.CountryIdentity;

                isSuccess = command.ExecuteNonQuery() > 0;

                if (isSuccess)
                    transaction.Commit();
                else
                    transaction.Rollback();
            }
            catch (SqliteException)
            {
                // Nota: Si usas 'using var transaction', no necesitas hacer el Rollback manual en el catch.
                // Si la transacción no llega al .Commit(), al destruirse hace Rollback automáticamente.
                throw;
            }

            return isSuccess;
        }

        /// <summary>
        /// Elimina el registro del usuario actual en la base de datos.
        /// </summary>
        /// <param name="user">La entidad User a eliminar de la base de datos.</param>
        /// <returns><see langword="bool"/>Devuelve true si se ha procesado correctamente ; false en caso contrario.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            string SQL_DELETE = "UPDATE User SET Active = @Active WHERE ID = @Id;";
            bool isSuccess = false;

            try
            {
                using var connection = new SqliteConnection(_connectionStringService.GetConnectionString());
                connection.Open();

                using var transaction = connection.BeginTransaction();
                using var command = new SqliteCommand(SQL_DELETE, connection, transaction);

                command.Parameters.Add("@Id", SqliteType.Integer).Value = user.Id;
                command.Parameters.Add("@Active", SqliteType.Integer).Value = 0;

                isSuccess = command.ExecuteNonQuery() > 0;

                if (isSuccess == true)
                    transaction.Commit();
                else
                    transaction.Rollback();

            }
            catch (SqliteException)
            {
                throw;
            }
            return isSuccess;
        }

        #endregion
    }
}