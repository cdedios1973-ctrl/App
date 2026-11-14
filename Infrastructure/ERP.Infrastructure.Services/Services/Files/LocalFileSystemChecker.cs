using System;
using System.IO;

namespace ERP.Infrastructure.Services.Files;

public interface IFileSystemChecker
{
    bool FileExists(string path);
    bool FileExists(string directory, string fileName);
    bool DirectoryExists(string path);
}

internal sealed class LocalFileSystemChecker : IFileSystemChecker
{
    public bool FileExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(path));

        return File.Exists(path);
    }

    public bool FileExists(string directory, string fileName)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("El directorio no puede estar vacío.", nameof(directory));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(fileName));

        return FileExists(Path.Combine(directory, fileName));
    }

    public bool DirectoryExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("La ruta del directorio no puede estar vacía.", nameof(path));

        return Directory.Exists(path);
    }
}

