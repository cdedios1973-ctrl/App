using System;
using ERP.Domain.Queries;

namespace ERP.Application.Interfaces.LoginService;

public interface ILoginService
{
    void Execute(UserLoginQueryDto userToLogin);

}
