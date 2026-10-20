using System;

namespace App.Definitions.Entity
{
    public sealed class User
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _surname;
        private readonly string _address;
        private readonly string _countryIdentity;

        #region Constructors

        public User(long id, string name, string surname, string address, string countryIdentity) 
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentException("User Name and Surname must be filled");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("User Name must be filled");
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentException("User Surname must be filled");
            }

            this._id = id;
            this._name = name;
            this._surname = surname;
            this._address = address;
            this._countryIdentity = countryIdentity;
        }

        #endregion

        #region Properties

        public long Id
        {   
            get { return this._id; }
        }

        public string Name
        { 
            get { return this._name; }
        }

        public string Surname
        { 
            get { return this._surname; }
        }

        public string Address
        { 
            get { return this._address; }
        }

        public string CountryIdentity
        {
            get { return this._countryIdentity; }
        }

        #endregion

    }
}
