using System;

using App.Infraestructure.CORE.Interfaces.Database;

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    /// <summary>
    /// Esta interfaz define las acciones básicas para guardar, cambiar o borrar datos en la base de datos.
    /// </summary>
    public interface ICommandDAO
    {
        /// <summary>
        /// Guarda un registro nuevo.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns>Devuelve true si el registro se insertó correctamente; de lo contrario, false.</returns>
        bool Insert(IDbExecutor db);

        /// <summary>
        /// Modifica un registro existente.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns>Devuelve true si el registro se modificó correctamente; de lo contrario, false.</returns>
        bool Update(IDbExecutor db);

        /// <summary>
        /// Borra un registro.
        /// </summary>
        /// <param name="db">El conector de la base de datos.</param>
        /// <returns>Devuelve true si el registro se eliminó correctamente; de lo contrario, false.</returns>
        bool Delete(IDbExecutor db);

    }
}