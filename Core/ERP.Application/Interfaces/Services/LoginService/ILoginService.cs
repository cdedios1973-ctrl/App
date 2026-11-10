using System;
using ERP.Domain.Queries;

namespace ERP.Application.Interfaces.LoginService;

public interface ILoginService
{
    bool Execute(UserLoginQueryDto userToLogin);

}
