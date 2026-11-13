using System;
using ERP.Domain.Queries;
using ERP.Application.DTO.Auth;

namespace ERP.Application.Interfaces.Persistence;

public interface IAuthUserReadRepository
{
    public AuthUserDto? Find(UserLoginQueryDto dto);

}