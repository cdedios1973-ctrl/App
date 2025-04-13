using System;
using App.Definitions.Enums;


namespace App.Definitions.Filters
{

    // first filter = WHERE - HAVING (two classes)
    // second filter = AND - OR

    // TODO : Se debe tener una plantilla "WHERE LIKE/=" si o si.
    // TODO : plantilla 0  = obligatorio WHERE
    // TODO : plantilla 1 = AND OR 


    public sealed class DataFilterList
    {
        private List<DataFilter> _filter = new List<DataFilter>();

        #region Constructors

        public DataFilterList(SQLClausule clausule, string fieldName, SQLSyntax syntax, string value)
        {
            this.AddFilter(clausule, fieldName, syntax, value);
        }

        #endregion

        #region Properties

        public List<DataFilter> GetFilter
        {
            get { return this._filter; }
        }

        #endregion

        #region Methods: Add filters && Clear

        public void AddFilter(SQLClausule clausule, string fieldName, SQLSyntax syntax, string value)
        {
            this._filter.Add(new DataFilter(clausule, fieldName, syntax, value));
        }

        public void Clear()
        {
            this._filter.Clear();
        }

        #endregion

    }
}
