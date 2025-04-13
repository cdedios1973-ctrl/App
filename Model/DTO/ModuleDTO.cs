using System;

namespace App.View.Models
{
    public sealed class ModuleDTO
    {
        private readonly long _id = 0;
        private readonly string _name;
        private readonly string _moduleName;
        private readonly string _observation;

        #region Constructors
        public ModuleDTO(long id, string name, string moduleName, string observation)
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
        public string ModuleName
        {
            get { return this._moduleName; }
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