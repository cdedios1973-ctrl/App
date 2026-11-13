using System;

namespace ERP.Application.Interfaces.Security;

internal interface IWindowsEnvironmentContext
{
    public string? GetMachineIdentity();
    public string? GetCurrentUserIdentity();

}
