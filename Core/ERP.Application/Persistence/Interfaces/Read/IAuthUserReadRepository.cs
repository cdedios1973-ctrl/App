using System;
using ERP.Domain.DTO;
using ERP.Domain.Auth;

namespace ERP.Application.Interfaces.Persistence;

public interface IAuthUserReadRepository
{
    AuthUserEntity Find(UserLoginQueryDto user);

}