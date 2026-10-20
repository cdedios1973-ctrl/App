using System;

using App.Definitions.Entity;
using Microsoft.Data.Sqlite;

namespace App.Infrastructure.Core.Interfaces.Dao
{
    public interface IPermissionDao
    {
        long Insert(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);
        long Update(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);
        bool Delete(Permission permissions, SqliteConnection connection, SqliteTransaction transaction);

    }
}