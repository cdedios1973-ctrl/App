using System;
using System.Xml;

using App.Infraestructure.CORE.Interfaces.Path;
using App.Infraestructure.CORE.Interfaces.Reader;

/// <summary>
/// Fixed usage: Return values of XML nodes.
/// </summary>

// TODO : Apply security (Microsfot security namespace, search & learn)
// UPGRADE : AT xmlDoc.Load(filePath) -> Could Create streamreader, 
// FIXED : Add param 'node' to have more flexibility.

namespace App.Infraestructure.CORE.Reader
{

    internal sealed class AppXmlReader : IAppXmlReader
    {
        //private const string MISSING_KEY    = "Incorrect Database type.";
        private const string MISSING_FILE   = "Config file cannot be found.";
        private const string MISSING_KEY    = "Key to search cannot be empty";
        private const string MISSING_VALUE  = "Key value is empty, verify config file";

        private readonly IAppPathProvider _pathProvider;
        private string _key;

        #region Constructors

        //internal AppXmlReader(IAppPathProvider pathProvider, DataBaseType dbType) 
        internal AppXmlReader(IAppPathProvider pathProvider) 
        {
            /*
            if (!Enum.IsDefined(typeof(DataBaseType), dbType))
            {
                throw new ArgumentException(MISSING_KEY, nameof(dbType));
            }
            this._key = dbType.ToString();
            */

            if (pathProvider == null)
                throw new ArgumentNullException(MISSING_FILE + "-", nameof(pathProvider));

            this._pathProvider = pathProvider;

        }

        #endregion

        #region Interface implementation
        
        public string GetValueByKey(string keyToSearch)
        {
            if (string.IsNullOrEmpty(keyToSearch))
                throw new ArgumentException(MISSING_KEY, "Key");

            string filePath = this._pathProvider.GetPath();

            if (string.IsNullOrEmpty(filePath))
                throw new FileNotFoundException(MISSING_FILE);

            string value = "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode node = xmlDoc.SelectSingleNode($"//appSettings/add[@key='{this._key}']");

            if (node != null && node.Attributes != null)
                value = node.Attributes["value"]?.Value;
            else 
                throw new ArgumentException(MISSING_VALUE);

            return value;
        }

        #endregion

    }
}
