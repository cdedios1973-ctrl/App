//using Microsoft.IdentityModel.Tokens;

using App.Definitions.Entity;
using App.Definitions.Interfaces.Config;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using Microsoft.Data.Sqlite;
using System;

// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).
// TODO : Arreglar la documentacion, voy a crear una clase modelo vacio como ejemplo, con la documentacion alli, para no repetir mucho y dejar el codigo claro
// TODO : Hash de campos.

namespace App.Infraestructure.Services.DAO.SqLite
{
    /// <summary>
    /// Proporciona la implementación de comandos SQL de escritura para la entidad <see cref="User"/> en SQLite.
    /// </summary>
    /// <remarks>
    /// Esta clase se enfoca en el uso de tipos primitivos compatibles con el proveedor de datos de SQLite.
    /// </remarks>
    public class SqliteUserDao : IUserDao //, IToken
    {
        //private readonly IConnectionStringService _connectionStringService;
        private const string SQL_INSERT = "INSERT INTO Users (Username, Surname) VALUES (@Name, @Surname); SELECT last_insert_rowid();";
        // TODO: Implementar constante sql de modificación y eliminacion
        //private const string SQL_UPDATE = "";
        //private const string SQL_DELETE = "";

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SqliteUserDao"/> con el usuario a procesar.
        /// </summary>
        /// <param name="user">La entidad de usuario que contiene los datos para las operaciones.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando el parámetro <paramref name="user"/> es nulo.</exception>
        public SqliteUserDao()// IConnectionStringService connectionStringService)
        {
            //this._connectionStringService = connectionStringService;
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
        public long Insert(User user, SqliteConnection connection, SqliteTransaction transaction)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            long lid = 0;
            SqliteCommand cmd = null;

            try
            {
                cmd = new SqliteCommand(SQL_INSERT, connection, transaction);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Surname", user.Surname);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    lid = Convert.ToInt64(result);
            }
            catch (SqliteException)
            {
                throw;
            }
            finally
            {
                if (cmd != null)
                    ((IDisposable)cmd).Dispose();
            }
            return lid;
        }

        /// <summary>
        /// Modifica el registro del usuario actual en la base de datos.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns><see langword="true"/> si el registro se modificó correctamente; de lo contrario, <see langword="false"/>.</returns>
        /// <remarks>
        /// **Seguridad:** Requiere nivel de permiso 2 o superior.
        /// </remarks>
        public long Update(User user, SqliteConnection connection, SqliteTransaction transaction)
        {
            throw new NotImplementedException();
        }

    /// <summary>
    /// Elimina el registro del usuario actual de la base de datos.
    /// </summary>
    /// <param name="db">El conector de la base de datos.</param>
    /// <returns><see langword="true"/> si el registro se eliminó correctamente; de lo contrario, <see langword="false"/>.</returns>
    /// <remarks>
    /// **Seguridad:** Requiere nivel de permiso 2 o superior.
    /// </remarks>
    public bool Delete(User user, SqliteConnection connection, SqliteTransaction transaction)
    {
        throw new NotImplementedException();
    }

        #endregion

    }
}