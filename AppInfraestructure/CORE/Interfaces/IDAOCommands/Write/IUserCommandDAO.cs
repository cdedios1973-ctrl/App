using App.Definitions.Entity;
using Microsoft.Data.Sqlite;
using System;

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    /// <summary>
    /// Esta interfaz define las acciones básicas para guardar, cambiar o borrar datos en la base de datos.
    /// </summary>
    public interface IUserCommandDAO
    {
        /// <summary>
        /// Guarda un registro nuevo.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve el Id del registro si el registro se insertó correctamente; de lo contrario, false.</returns>
        long InsertTransactional(User user, SqliteConnection connection, SqliteTransaction transaction);

        /// <summary>
        /// Modifica un registro existente.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve el Id del registro si se modificó correctamente.</returns>
        long UpdateTransactional(User user, SqliteConnection connection, SqliteTransaction transaction);
        /// <summary>
        /// Borra un registro.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve true si el registro se eliminó correctamente; de lo contrario, false.</returns>
        bool DeleteTransactional(User user, SqliteConnection connection, SqliteTransaction transaction);

    }
}