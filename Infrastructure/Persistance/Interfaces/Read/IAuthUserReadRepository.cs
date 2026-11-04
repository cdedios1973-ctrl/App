using System;

using App.Definitions.DTO;
using App.Definitions.Auth;

namespace App.Infrastructure.Persistence
{
    public interface IAuthUserReadRepository
    {
        AuthUserEntity Find(UserLoginQueryDto user);

    }

}