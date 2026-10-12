using System;
using App.Definitions.Enums;

namespace App.Definitions.Filters
{
    /*  Class designed to add parameters to sql queries */

    public sealed class DataFilter
    {
        private readonly string _clausule;
        private readonly string _fieldName;
        private readonly string _syntax;
        private readonly string _value;

        #region Constructor

        public DataFilter(SQLClausule clausule, string fieldName, SQLSyntax syntax, string value)
        {
            if (string.IsNullOrEmpty(fieldName))
            { 
                throw new ArgumentNullException(nameof(fieldName));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            this._fieldName = fieldName;
            this._syntax = syntax.ToString();
            this._value = value;
            this._clausule = clausule.ToString();
        }

        #endregion

        #region Properties

        public string Clausule
        { 
            get { return this._clausule; } 
        }

        public string FieldName
        { 
            get { return this._fieldName; } 
        }

        public string Syntax
        { 
            get { return this._syntax; } 
        }

        public string Value
        { 
            get { return this._value; } 
        }

        #endregion

    }

}
