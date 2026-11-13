using System;

namespace ERP.Domain.Interfaces.Security;

public interface IEnvironmentContext
{
    public string? GetMachineIdentity();
    public string? GetCurrentUserIdentity();

}
