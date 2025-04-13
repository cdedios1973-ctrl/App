using System;
using System.Xml;

using App.Definitions.Enums;
using App.Infraestructure.CORE.Interfaces.Reader;

namespace App.Infraestructure.CORE.Reader
{
    /// <summary>
    /// Fixed usage: Return values of XML nodes, just connection strings selected by DataBaseType dbType.
    /// </summary>

    // TODO : Can add param 'node' to have more flexibility, not the case now.
    // TODO : AT xmlDoc.Load(filePath) -> Create fileStrean, wich is better because can close the stream at finally. try / ctach block 
    // TODO : Apply security (Microsfot security namespace, search & learn)

    internal sealed class AppXmlReader : IAppXmlReader
    {
        private const string MISSING_FILE   = "Config file cannot be found.";
        private const string MISSING_KEY    = "Incorrect Database type.";
        //private const string MISSING_KEY    = "Key to search cannot be empty";
        private const string MISSING_VALUE  = "Key value is empty, verify config file";

        private readonly IAppConfigPathProvider _pathProvider;
        private string _key;

        #region Constructors

        internal AppXmlReader(IAppConfigPathProvider pathProvider, DataBaseType dbType) 
        {
            if (pathProvider == null)
            {
                throw new ArgumentNullException(MISSING_FILE + "-", nameof(pathProvider));
            }
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
            {
                throw new ArgumentException(MISSING_KEY, nameof(dbType));
            }

            this._pathProvider = pathProvider;
            this._key = dbType.ToString();
        }

        #endregion

        #region Interface implementation
        
        public string GetValueByKey()
        {
            string filePath = this._pathProvider.GetPath();

            if (string.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException(MISSING_FILE);
            }

            string value = "";

            // TODO : xmlDoc.Load( NEED A READER! i totally forgot )

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode node = xmlDoc.SelectSingleNode($"//appSettings/add[@key='{this._key}']");

            if (node != null && node.Attributes != null)
            {
                value = node.Attributes["value"]?.Value;
            }
            else 
            {
                throw new ArgumentException(MISSING_VALUE);
            }

            return value;
        }

        #endregion

    }
}
