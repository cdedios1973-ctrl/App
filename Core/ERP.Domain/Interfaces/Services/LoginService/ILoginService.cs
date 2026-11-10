using System;
using ERP.Domain.DTO;

namespace ERP.Domain.Interfaces.Login;

public interface ILoginService
{
    bool Login(UserLoginQueryDto userToLogin);
}
