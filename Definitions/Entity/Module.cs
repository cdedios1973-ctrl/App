using System;

namespace App.Definitions.Entity
{
    public sealed class Module
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _moduleName;
        private readonly string _observation;

        #region Constructors

        public Module(long id, string name, string moduleName, string observation)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be filled");
            }
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                throw new ArgumentException("Module name must be filled");
            }

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
            private set { }
        }

        internal string Name
        {
            get { return this._name; }
            private set { }
        }

        internal string ModuleName
        {
            get { return this._moduleName; }
            private set { }
        }

        internal string Observation
        {
            get { return this._observation; }
            private set { }
        }

        #endregion

    }
}