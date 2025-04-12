namespace App.Models
{
    using System;
    using App.Enums;
    using App.Interfaces;

    [Serializable()]
    public sealed class Tasks : IModel
    {
        private long _id = 0;
        private string _name = "";
        private string _observation = "";

        #region Constructors

        public Tasks() { }

        public Tasks(long id, string name, string observation) 
        {
            this._id = id;
            this._name = name;
            this._observation = observation;
        }

        #endregion

        #region Properties

        public long Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public string Observation
        {
            get { return this._observation; }
            set { this._observation = value; }
        }

        #endregion

        #region Interface implementation

        public string IGetTableName()
        {
            return "Tasks";
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
            return new ModuleData { id = this._id, name = "Tasks" };
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