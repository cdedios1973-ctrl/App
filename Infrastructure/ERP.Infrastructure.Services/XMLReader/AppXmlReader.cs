using System;
using System.Xml;
using ERP.Domain.Entity.AppConfig;
using ERP.Infrastructure.Services.Interfaces;
// ============================================================================
// REFACTORING LOG & ROADMAP
// ============================================================================
// DONE : Modificar AppXmlReader, implementar parammetro keyToSearch para ganar flexibilidad.
// DONE : Despues del cambio del servicio de rutas, tengo que buscar en la lista de rutas de aplicacion por delegado.
// DONE : Actualmente se está apuntando a un archivo fisico estatico en el disco.//
// TODO : El nombre del archivo es estatico.
// TODO : Al igual que el servicio de rutas este proceso se tiene que lanzar al inicio de la aplicacion, no hay rutas dinamicas al vuelo, solo se lee una vez
//
// TODO : Aplicar seguridad de Windows (System.Security.Principal) (Es prioritario, despues del inicio de aplicacion)
// TODO : Podria usar streamreader en el xmldoc.Load(filepatth) (Pospuesto)
// ============================================================================

namespace ERP.Infrastructure.Services.Reader;

/// <summary>
/// Fixed usage: Return value of XML node.
/// </summary>
internal sealed class AppXmlReader : IAppXmlReader
{
    private const string MISSING_FILE   = "Config file cannot be found.";
    private const string MISSING_KEY    = "Key to search cannot be empty";
    private const string MISSING_VALUE  = "Key value is empty, verify config file";
    private readonly IStorageConfig _storageConfig;

    internal AppXmlReader(IStorageConfig storegeConfig) 
    {
        if (storegeConfig == null)
            throw new ArgumentNullException(MISSING_FILE + "-", nameof(storegeConfig));

        this._storageConfig = storegeConfig;
    }
   
    public string GetValueByKey(string keyToSearch)
    {
        if (string.IsNullOrEmpty(keyToSearch))
            throw new ArgumentException(MISSING_KEY, "Key");

        string path = this._storageConfig.LocalXMLPath;

        // TODO : El nombre del archivo ahora mismo es estatico.
        string file = Path.Combine(path, "config.xml");

        string value = "";

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(file);

        XmlNode node = xmlDoc.SelectSingleNode($"//appSettings/add[@key='{keyToSearch}']");

        if (node != null && node.Attributes != null)
            value = node.Attributes["value"]?.Value;
        else 
            throw new ArgumentException(MISSING_VALUE);

        return value;
    }

}
