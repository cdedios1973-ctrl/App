using System;

namespace App.View.Models
{
    internal sealed class PermissionsDTO
    {
        private readonly long _id = 0;
        private readonly string _parent;
        private readonly string _name;
        private readonly string _observation;

        #region Constructors
        internal PermissionsDTO(long id, string parent, string name, string observation)
        {
            if (string.IsNullOrWhiteSpace(parent))
            {
                throw new ArgumentException("Parent must be filled");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be filled");
            }

            this._id = id;
            this._parent = parent;
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
        public string Parent
        {
            get { return this._parent; }
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