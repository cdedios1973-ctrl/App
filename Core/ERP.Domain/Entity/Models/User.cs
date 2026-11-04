using System;

/// <summary>
/// POCO de la tabla maestra 'User' (Logic - Persistence).
/// Mantenenido en formato previo de Visual Studio
/// </summary>

namespace ERP.Domain.Entity
{
    public sealed class User
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _surname;
        private readonly string _address;
        private readonly string _countryIdentity;

        #region Constructor

        public User(long id, string name, string surname, string address, string countryIdentity) 
        {
            this._id = id;
            this._name = name;
            this._surname = surname;
            this._address = address;
            this._countryIdentity = countryIdentity;
        }

        #endregion

        #region Properties { Readonly }

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
