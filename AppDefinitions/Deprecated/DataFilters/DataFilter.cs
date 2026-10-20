using System;
using App.Definitions.Enums;

namespace AppDefinitions.Deprecated.DataFilters
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

            _fieldName = fieldName;
            _syntax = syntax.ToString();
            _value = value;
            _clausule = clausule.ToString();
        }

        #endregion

        #region Properties

        public string Clausule
        { 
            get { return _clausule; } 
        }

        public string FieldName
        { 
            get { return _fieldName; } 
        }

        public string Syntax
        { 
            get { return _syntax; } 
        }

        public string Value
        { 
            get { return _value; } 
        }

        #endregion

    }

}
