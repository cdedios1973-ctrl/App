using System;

namespace ERP.Domain.Interfaces.Security;

public interface ISessionContext
{
    public string? GetCurrenMachine();
    public string? GetCurrenUserName();
    public long GetCurrentUserId();
    bool IsAuthenticated { get; }
    void SetUser(long userId, string userName, string machineName);
    void Clear();
}
