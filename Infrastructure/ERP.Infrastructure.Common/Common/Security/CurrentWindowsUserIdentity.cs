using System;
using System.Security.Principal;

namespace ERP.Infrastructure.Common.Security
{
    public static class CurrentWindowsUserIdentity
    {
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
    }
}