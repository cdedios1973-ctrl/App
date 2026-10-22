using System;

using Microsoft.Data.Sqlite;
using App.Definitions.Entity;

namespace App.Infrastructure.Persistence
{
    public interface IPermissionWriteRepository
    {
        long Insert(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);
        long Update(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);
        bool Delete(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);

    }
}