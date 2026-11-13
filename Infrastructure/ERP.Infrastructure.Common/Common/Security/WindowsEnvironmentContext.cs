using System;
using System.Security.Principal;
using ERP.Domain.Interfaces.Security;

namespace ERP.Infrastructure.Common.Security;

public sealed class WindowsEnvironmentContext : IEnvironmentContext
{
    //private readonly WindowsPrincipal _principal;
    private readonly WindowsIdentity _windowsIdentity;

    public WindowsEnvironmentContext() 
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException("El sistema operativo actual no es compatible.");

        this._windowsIdentity = WindowsIdentity.GetCurrent();
    }

    public string? GetCurrentUserIdentity()
    {
        return this._windowsIdentity.Name.ToUpperInvariant() ?? string.Empty;
    }

    public string? GetMachineIdentity()
    {
        return Environment.MachineName.ToUpperInvariant();
    }

    /*
    public static string GetCurrentWindowsUserName()
    {
        if (OperatingSystem.IsWindows())
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            string currentUser = windowsIdentity.Name.ToUpperInvariant();

            return currentUser ?? string.Empty;
        }
        return string.Empty;
    }
    */

}