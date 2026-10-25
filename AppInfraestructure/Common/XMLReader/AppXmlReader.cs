using System;
using System.Xml;

using App.Definitions.DTO;
using App.Definitions.Predicates;
using TaskHub.Infrastructure.Paths;

// ============================================================================
// REFACTORING LOG & ROADMAP
// ============================================================================
// DONE : Modificar AppXmlReader, implementar parammetro keyToSearch para ganar flexibilidad.
// DONE : Despues del cambio del servicio de rutas, tengo que buscar en la lista de rutas de aplicacion por delegado.
//
// TODO : Al igual que el servicio de rutas este proceso se tiene que lanzar al inicio de la aplicacion, no hay rutas dinamicas al vuelo, solo se lee una vez
// TODO : Aplicar seguridad de Windows (System.Security.Principal) (Es prioritario, despues del inicio de aplicacion)
// TODO : Podria usar streamreader en el xmldoc.Load(filepatth) (Pospuesto)
// ============================================================================

namespace TaskHub.Infrastructure.Reader
{
    /// <summary>
    /// Fixed usage: Return value of XML node.
    /// </summary>
    internal sealed class AppXmlReader : IAppXmlReader
    {
        private const string MISSING_FILE   = "Config file cannot be found.";
        private const string MISSING_KEY    = "Key to search cannot be empty";
        private const string MISSING_VALUE  = "Key value is empty, verify config file";
        private readonly IPathProvider _pathProvider;

        #region Constructors

        internal AppXmlReader(IPathProvider pathProvider) 
        {
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

            // solucion temporal mientras creo la clase contenedora de las rutas para que se lea solo una vez al inicio de la aplcaicion
            List<PathInfo> paths = this._pathProvider.GetAppGlobalPaths();
            PathInfo file = paths.Find(PathInfoFilters.WithKey("cfg"));

            if (file == null)
                throw new ArgumentException(MISSING_KEY, "Key");

            string value = "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(file.Path, file.FileName));

            XmlNode node = xmlDoc.SelectSingleNode($"//appSettings/add[@key='{keyToSearch}']");

            if (node != null && node.Attributes != null)
                value = node.Attributes["value"]?.Value;
            else 
                throw new ArgumentException(MISSING_VALUE);

            return value;
        }

        #endregion

    }
}
