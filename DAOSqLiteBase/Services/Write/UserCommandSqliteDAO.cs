using System;
using Microsoft.Data.Sqlite;
//using Microsoft.IdentityModel.Tokens;

using App.Definitions.Entity;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using App.Infraestructure.CORE.Interfaces.Database;

// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).

namespace App.Infraestructure.Services.DAO.SqLite
{
    /// <summary>
    /// Proporciona la implementación de comandos SQL de escritura para la entidad <see cref="User"/> en SQLite.
    /// </summary>
    /// <remarks>
    /// Esta clase se enfoca en el uso de tipos primitivos compatibles con el proveedor de datos de SQLite.
    /// </remarks>
    public class UserCommandSqliteDAO : ICommandDAO //, IToken
    {
        private const string SQL_INSERT = "INSERT INTO Users (Username, PasswordHash) VALUES (@Name, @Surname);";
        //private const string SQL_LAST_ID = "SELECT last_insert_rowid();";

        // TODO: Implementar constante sql de modificación
        //private const string SQL_UPDATE = "";

        // TODO: Implementar constante sql de eliminación
        //private const string SQL_DELETE = "";

        private const string EMPTY_CONSTRUCTOR = "Empty User. Nothing to save.";

        private readonly User _user;

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserCommandSqliteDAO"/> con el usuario a procesar.
        /// </summary>
        /// <param name="user">La entidad de usuario que contiene los datos para las operaciones.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando el parámetro <paramref name="user"/> es nulo.</exception>
        
        public UserCommandSqliteDAO(User user)
        {
            if (user == null) 
                throw new ArgumentNullException(nameof(user), EMPTY_CONSTRUCTOR); 

            this._user = user;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Inserta el registro del usuario actual en la base de datos.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns><see langword="true"/> si el registro se insertó correctamente; de lo contrario, <see langword="false"/>.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Insert(IDbExecutor db) // Permission needed >= 2
        {
            // TODO : IMPORTANT -> don forget try / catch at logic Service.
            if (db == null) 
                throw new ArgumentNullException(nameof(db));

            bool result = false;
            using (var cmd = new SqliteCommand(SQL_INSERT))
            {
                cmd.Parameters.AddWithValue("@Name", _user.Name);

                // NOTA IMPORTANTE: Me tengo q asegurar de guardar el Surname en PasswordHash.
                cmd.Parameters.AddWithValue("@Surname", _user.Surname);

                result = (db.ExecNonQuery(cmd) > 0);
            }
            return result;
        }

        /// <summary>
        /// Modifica el registro del usuario actual en la base de datos.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns><see langword="true"/> si el registro se modificó correctamente; de lo contrario, <see langword="false"/>.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Update(IDbExecutor db)
        {
            // TODO: Implementar lógica de modificación
            return false;
        }

        /// <summary>
        /// Elimina el registro del usuario actual de la base de datos.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns><see langword="true"/> si el registro se eliminó correctamente; de lo contrario, <see langword="false"/>.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public bool Delete(IDbExecutor db)
        {
            // TODO: Implementar lógica de eliminación
            return false;
        }

        #endregion

    }
}