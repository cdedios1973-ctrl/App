using System;

/// <summary>
/// Modelo Visual Studio versiones anteriores
/// DTO de la tabla maestra 'User' (Vista).
/// Nota de diseño: Se conserva este formato clásico (readonly + get puro con campos privados) como referencia histórica. 
/// En la refactorización final hacia Native AOT, se migra hacia record class con init para eliminar la necesidad de reflexión dinámica y suprimir el atributo 
/// [Serializable] obsoleto
/// </summary>

namespace App.Definitions.DTO
{
    //[Serializable()]
    public sealed class UserDto
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _surname;
        private readonly string _address;
        private readonly string _countryIdentity;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserDto"/>.
        /// </summary>
        /// <param name="id">User.Id (PK)</param>
        /// <param name="name">User.Name</param>
        /// <param name="surname">User.Surname</param>
        /// <param name="address">User.Address</param>
        /// <param name="countryIdentity">User.CountryIdentity</param>

        #region Constructor
        public UserDto(long id, string name, string surname, string address, string countryIdentity) 
        {
            this._id = id;
            this._name = name;
            this._surname = surname;
            this._address = address;
            this._countryIdentity = countryIdentity;
        }

        #endregion

        #region Properties { immutable, locked after creation }
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