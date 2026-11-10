using System;

using ERP.Domain.Auth;

namespace ERP.Application.Interfaces.Persistence;

public interface IAuthUserReadRepository
{
    UserAuthQueryDto Find(string userName, string password);

}