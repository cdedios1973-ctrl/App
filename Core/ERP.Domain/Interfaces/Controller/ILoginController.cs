using System;
using System.Collections.Generic;
using ERP.Domain.DTO;

namespace ERP.Domain.Interfaces.Controller;

public interface ILoginController
{
    List<string> Login(UserLoginQueryDto dto);

}

