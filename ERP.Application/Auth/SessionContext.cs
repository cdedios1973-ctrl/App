using System;
using ERP.Domain.Interfaces.Security;

namespace ERP.Application.Auth;

public sealed class SessionContext : ISessionContext
{
    private AuthUser? _currentUser;

    // TODO: Nueva sintaxis : public bool IsAuthenticated => _currentUser is not null;
    public bool IsAuthenticated 
    {
        get { return (this._currentUser != null); }
    }

    public void SetUser(long userId, string userName, string machineName)
    {
        _currentUser = new AuthUser(userId, userName, machineName);
    }

    #region Interface Implementation

    public void Clear()
    { 
        this._currentUser = null;
    }

    public long GetCurrentUserId()
    { 
        return this._currentUser?.UserId ?? 0;
    }
    
    public string? GetCurrenUserName()
    { 
        return _currentUser?.UserName ?? string.Empty;
    }
    
    public string? GetCurrenMachine()
    {
        return this._currentUser?.MachineName ?? string.Empty;
    }

    #endregion

}

