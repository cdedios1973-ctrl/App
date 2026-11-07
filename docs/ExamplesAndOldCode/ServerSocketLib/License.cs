using System;
using System.Collections.Generic;
using System.Text;

using IDAO.Attributes;
using IDAO.Interfaces;
using Logic;

namespace ServerSocketLib 
{
    [DAOClass("Licenses", "Licencia", "Licencias")]
    public class License : IDbClass
    {
        private long    _id;
        private String  _name;
        private String  _cif;
        private String  _hdSerial;
        private Int16   _active = 0;
        private DateTime _initDate;
        private DateTime _endDate;

        public License() { }

        [DAO("id", typeof(long), true)]
        public long Id 
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [DAO("name", typeof(String))]
        public String Name 
        {
            get { return this._name; }
            set { this._name = value; }
        }

        [DAO("cif", typeof(String))]
        public String CIF
        {
            get { return this._cif; }
            set { this._cif = value; }
        }

        [DAO("hdSerial", typeof(String))]
        public String HDSerial 
        {
            get { return this._hdSerial; }
            set { this._hdSerial = value; }
        }

        [DAO("active", typeof(Int16))]
        public Int16 Active 
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DAO("initdate", typeof(DateTime))]
        public DateTime InitDate
        {
            get { return this._initDate; }
            set { this._initDate = value; }
        }

        [DAO("enddate", typeof(DateTime))]
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { this._endDate = value; }
        }

        #region Miembros de IDbClass

        public short GetDataStatus() 
        {
            return 0;
        }

        public void SetDataStatus(short status)
        {
        }

        public long GetId() 
        {
            return this._id;
        }

        public string GetName() 
        {
            return this._name;
        }

        #endregion
    }
}
