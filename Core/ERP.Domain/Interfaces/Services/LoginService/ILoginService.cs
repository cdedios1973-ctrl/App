using System;
using System.Collections.Generic;
using ERP.Domain.DTO;

namespace ERP.Domain.Interfaces.Login;

public interface ILoginService
{
    List<string> Login(UserLoginQueryDto userToLogin);
}
