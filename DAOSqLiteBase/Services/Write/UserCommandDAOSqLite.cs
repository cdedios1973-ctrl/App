using System;
using Microsoft.Data.Sqlite;
//using Microsoft.IdentityModel.Tokens;

using App.Definitions.Entity;
using App.Infraestructure.CORE.Interfaces.DAOCommands;
using App.Infraestructure.CORE.Interfaces.Database;
using App.Infraestructure.CORE.Interfaces.Reader;

// TODO : Make summary -> App aim to use native types.
// UPGRADE : Write DAO -> diferent folder than Select DAO ( For code clarity, and security SELECT / CRUD )
// TODO : Can make a List<SqliteCommand> here and send to database class.
// SEARCH BETTER OPTION : List<SqliteCommand>[0] master data.
// SEARCH BETTER OPTION : List<SqliteCommand>[1] detail data. linked by master_data_id
// SEARCH BETTER OPTION : Both List<SqliteCommand>[x] to DAOSqliteBase -> directly to transaction (2 foreach, check if second list has items)
// TODO : Fix return for lastId.
// UPGRADE : Token permission, permission needed >= 2 (Search microsoft documentation security roles).

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

        // TODO : I can change constructor to admit List<Items> not in this class (EX.: List<BillDetails> )
        public UserCommandDAOSqLite(User user)
        {
            if (user == null) 
            { 
                throw new ArgumentNullException(EMPTY_CONSTRUCTOR); 
            }
            this._user = user;
        }

        #endregion

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
