using System;

using App.Definitions.Entity;

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    /// <summary>
    /// Esta interfaz define las acciones básicas para guardar, cambiar o borrar datos en la base de datos.
    /// </summary>
    public interface IModuleCommandDAO
    {
        /// <summary>
        /// Guarda un registro nuevo.
        /// </summary>
        /// <param name="module">Procesa la entidad Module.</param>
        /// <returns>Devuelve true si el registro se insertó correctamente; de lo contrario, false.</returns>

        bool Insert(Module module);

        /// <summary>
        /// Modifica un registro existente.
        /// </summary>
        /// <param name="module">Procesa la entidad Module.</param>
        /// <returns>Devuelve true si el registro se modificó correctamente; de lo contrario, false.</returns>
        bool Update(Module module);

        /// <summary>
        /// Borra un registro.
        /// </summary>
        /// <param name="module">Procesa la entidad Module.</param>
        /// <returns>Devuelve true si el registro se eliminó correctamente; de lo contrario, false.</returns>
        bool Delete(Module module);

    }
}