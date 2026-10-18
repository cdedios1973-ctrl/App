using System;

namespace App.Definitions.Entity
{
    public sealed class Tasks
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _observation;

        #region Constructors

        public Tasks(long id, string name, string observation) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be filled");
            }

            this._id = id;
            this._name = name;
            this._observation = observation;
        }

        #endregion

        #region Properties

        public long Id
        {
            get { return this._id; }
            private set { }
        }
        public string Name
        {
            get { return this._name; }
            private set { }
        }
        public string Observation
        {
            get { return this._observation; }
            private set { }
        }

        #endregion

    }
}