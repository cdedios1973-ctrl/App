using System;

using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

// TODO : arreglar documentacion

namespace App.Infrastructure.Persistence
{
    /// <summary>
    /// Esta interfaz define las acciones básicas para guardar, cambiar o borrar datos en la base de datos.
    /// </summary>
    public interface IUserWriteRepository
    {
        /// <summary>
        /// Guarda un registro nuevo.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve true si el registro se insertó correctamente; de lo contrario false.</returns>
        bool Insert(User user);

        /// <summary>
        /// Modifica un registro existente.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve true si el registro se modificó correctamente; de lo contrario false.</returns>
        bool Update(User user);
        /// <summary>
        /// Borra un registro.
        /// </summary>
        /// <param name="user">Procesa la entidad User.</param>
        /// <returns>Devuelve true si el registro se eliminó correctamente; de lo contrario false.</returns>
        bool Delete(User user);

    }
}