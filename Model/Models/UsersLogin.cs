namespace App.Models
{
    using System;
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    public sealed class UsersLogin : IModel
    {
        private long _id = 0;
        private long _userId = 0;
        private string _username = "";
        private string _password = "";

        public UsersLogin() { }

        public UsersLogin(long id, long userId, string username, string password)
        {
            this._id = id;
            this._userId = userId;
            this._username = username;
            this._password = password;
        }

        /*
        public static UsersLogin Create(IDataRecord record)
        {
            return new UsersLogin
            {
                Id = (int)record["id"],
                UserId = (int)record["userid"],
                Username = record["username"].ToString(),
                Password = record["password"].ToString()
            };
        }
        */

        #region Properties

        public long Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public long UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }
        public string UserName
        {
            get { return this._username; }
            set { this._username = value; }
        }
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "UsersLogin";
        }
        public long IGetId()
        {
            return this._id; ;
        }
        public string IGetSQLJoin()
        {
            return "null";
        }
        public ModuleData IGetModule()
        {
            return new ModuleData { id = this._id, name = "UsersLogin" };
        }

        public DFilter IGetFilterById(long Id)
        {
            return new DFilter("WHERE", "Id", "=", Id.ToString());
        }

        public DFilter? IGetFilterByUserId(long Id)
        {
            return new DFilter("WHERE", "UserId", "=", Id.ToString());
        }

        #endregion

    }
}
