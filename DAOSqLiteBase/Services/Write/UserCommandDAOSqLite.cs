using System;
using Microsoft.Data.Sqlite;
//using Microsoft.IdentityModel.Tokens;

using App.Definitions.Entity;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using App.Infraestructure.CORE.Interfaces.Database;
using App.Infraestructure.CORE.Interfaces.Reader;

// TODO : Write DAO -> diferent folder than Select DAO (File System access permission)
// TODO : Bulk -> in other tables. (List<POCO>) transaction, foreach, commit, rollback, finally->close.
// TODO : Send List<Items> to SqLiteBase (change constructor).
// TODO : This sort of class can be the orquestator, and call the database for the foreach.

// TODO : Make summary -> App aim to use native types.

namespace App.Infraestructure.Services.DAO.SqLite
{
    public class UserCommandDAOSqLite : IDAOCommands //, IToken
    {
        private const string SQL_INSERT = "INSERT INTO Users (Username, PasswordHash) VALUES (@Name, @Surname)";
        private const string SQL_UPDATE = "";
        private const string SQL_DELETE = "";
        private const string EMPTY_CONSTRUCTOR = "Empty User. Nothing to save.";

        private IDbExecutor _db;
        private IConnectionStringService _connectionString;
        private readonly User _user;

        #region Constructor

        public UserCommandDAOSqLite(User user)
        {
            if (user == null) 
            { 
                throw new ArgumentNullException(EMPTY_CONSTRUCTOR); 
            }
            this._user = user;
        }

        #endregion

        // TODO : I can make a List<SqliteCommand> here and send to database class.
        // TODO : Fix return (if lastId > 0) then true.
        // TODO : Token permission, permission needed >= 2 (Search microsoft documentation security roles).
        #region Interface Implementation

        public int Insert() // Permission needed >= 2
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.AddWithValue("@Name", this._user.Name);
            cmd.Parameters.AddWithValue("@Surname", this._user.Surname);

            this._db = new DAOSqLiteBase(cmd, this._connectionString);

            return (int)this._db.ExecuteCrud();
        }

        public int Update() // Permission needed >= 2
        {
            return 0;
        }

        public int Delete() // Permission needed >= 2
        {
            return 0;
        }

        #endregion

    }
}
