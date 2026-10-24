using System;

using App.Definitions.DTO;

namespace App.Definitions.Interfaces.Login
{
    public interface ILoginService
    {
        bool Login(UserLoginQueryDTO userToLogin);

    }
}
