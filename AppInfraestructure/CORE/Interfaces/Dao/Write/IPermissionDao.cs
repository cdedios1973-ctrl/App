using System;

using App.Definitions.Entity;
using Microsoft.Data.Sqlite;

namespace App.Infrastructure.Core.Interfaces.Dao
{
    public interface IPermissionDao
    {
        long Insert(Permissions permissions, SqliteConnection connection, SqliteTransaction transaction);
        long Update(Permissions permissions, SqliteConnection connection, SqliteTransaction transaction);
        bool Delete(Permissions permissions, SqliteConnection connection, SqliteTransaction transaction);

    }
}