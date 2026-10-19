using System;

using App.Definitions.Entity;
using Microsoft.Data.Sqlite;

namespace App.Infraestructure.CORE.Interfaces.DAOCommands
{
    public interface IModuleDao
    {
        long Insert(Module module, SqliteConnection connection, SqliteTransaction transaction);
        long Update(Module module, SqliteConnection connection, SqliteTransaction transaction);
        bool Delete(Module module, SqliteConnection connection, SqliteTransaction transaction);

    }
}