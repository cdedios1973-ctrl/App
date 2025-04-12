namespace App.Models
{
    using System;
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    public sealed class Users : IModel
    {
        private long _id = 0;
        private string _name = "";
        private string _surname = "";
        private string _address = "";
        private string _countryIdentity = "";

        #region Constructors

        public Users() { }

        public Users(long id, string name, string surname, string address, string countryIdentity) 
        {
            this._id = id;
            this._name = name;
            this._surname = surname;
            this._address = address;
            this._countryIdentity = countryIdentity;
        }

        /*
        public static User Create(IDataRecord record)
        {
            return new User
            {
                Id = (int)record["id"],
                Name = record["name"].ToString(),
                Surname = record["surname"].ToString(),
                Address = record["address"].ToString()
            };
        }
        */
        #endregion

        #region Properties

        public long Id
        {   get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        { 
            get { return this._name; }
            set { this._name = value; }
        }

        public string Surname
        { 
            get { return this._surname; } 
            set { this._surname = value; } 
        }

        public string Address
        { 
            get { return this._address; } 
            set { this._address = value;  }
        }

        public string CountryIdentity
        {
            get { return this._countryIdentity; }
            set { this._countryIdentity = value; }
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "Users";
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
            return new ModuleData { id = this._id, name = "Users" };
        }

        public DFilter IGetFilterById(long Id)
        {
            return new DFilter("WHERE", "Id", "=", Id.ToString());
        }

        public DFilter? IGetFilterByUserId(long Id)
        {
            return null;
        }

        #endregion

    }
}
