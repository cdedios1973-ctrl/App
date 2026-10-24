using System;

using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

namespace App.Infrastructure.Persistence
{
    public interface IModuleWriteRepository
    {
        long Insert(Module module, SqliteConnection connection, SqliteTransaction transaction);
        long Update(Module module, SqliteConnection connection, SqliteTransaction transaction);
        bool Delete(Module module, SqliteConnection connection, SqliteTransaction transaction);

    }
}