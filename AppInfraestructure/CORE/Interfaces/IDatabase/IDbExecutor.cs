using System;
using Microsoft.Data.Sqlite;

namespace App.Infraestructure.CORE.Interfaces.Database
{
    public interface IDbExecutor
    {
        public bool ExecNonQuery(SqliteCommand master, List<SqliteCommand> details = null);
    
    }

}