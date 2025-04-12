namespace App.Models
{
    using System;
    using System.Runtime.CompilerServices;
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    internal sealed class Module : IModel
    {
        private long _id = 0;
        private string _name = "";
        private string _moduleName = "";
        private string _observation = "";

        #region Constructors

        internal Module() { }

        internal Module(long id, string name, string moduleName, string observation)
        {
            this._id = id;
            this._name = name;
            this._moduleName = moduleName;
            this._observation = observation;
        }

        #endregion

        #region Properties

        internal long Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        internal string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        internal string ModuleName
        {
            get { return this._moduleName; }
            set { this._moduleName = value; }
        }

        internal string Observation
        {
            get { return this._observation; }
            set { this._observation = value; }
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "Module";
        }
        public long IGetId()
        {
            return this._id; ;
        }
        public string IGetSQLJoin()
        {
            return null; 
        }

        public ModuleData IGetModule()
        {
            return new ModuleData { id = this._id, name = "Module" };
        }

        public DFilter IGetFilterById(long Id)
        {
            return new DFilter("WHERE", "ID", "=", Id.ToString());
        }

        public DFilter? IGetFilterByUserId(long Id)
        {
            return null;
        }

        #endregion

    }
}

