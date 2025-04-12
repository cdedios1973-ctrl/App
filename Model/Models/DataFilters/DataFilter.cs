namespace App.Models
{
    /*  Class designed to add parameters to sql where */
    using System;

    public sealed class DFilter
    {
        private string _clausule = "";
        private string _fieldName = "";
        private string _syntax = "";
        private string _value = "";

        #region Constructor

        internal DFilter(string clausule, string fieldName, string syntax, string value)
        {
            this._fieldName = fieldName;
            this._syntax = syntax;
            this._value = value;
            this._clausule = clausule;
        }

        internal DFilter(DFilter filter)
        {
            this._fieldName = filter.FieldName;
            this._syntax = filter.Syntax;
            this._value = filter.Value;
            this._clausule = filter.Clausule;
        }

        #endregion

        #region Properties
        public string Clausule
        { get { return this._clausule; } }

        public string FieldName
        { get { return this._fieldName; } }

        public string Syntax
        { get { return this._syntax; } }

        public string Value
        { get { return this._value; } }

        #endregion

    }

    public sealed class DataFilter 
    {
        private List<DFilter> _filter = new List<DFilter>();

        #region Constructors

        public DataFilter()
        { }

        public DataFilter(string clausule, string fieldName, string syntax, string value)
        {
            this.AddFilter(clausule, fieldName, syntax, value);
        }

        public DataFilter(DFilter filter)
        {
            this._filter.Add(filter);
        }

        #endregion

        #region Properties

        public List<DFilter> GetFilter
        { 
            get { return this._filter; } 
        }

        #endregion

        #region Methods: Add filters && Clear

        public void AddFilter(string clausule, string fieldName, string syntax, string value)
        {
            this._filter.Add(new DFilter(clausule, fieldName, syntax, value));
        }

        public void AddFilter(DFilter filter)
        {
            this._filter.Add(new DFilter(filter));
        }

        public void Clear()
        {
            this._filter.Clear();
        }

        #endregion

    }
}
