using System;

using App.Definitions.Entity;

namespace App.Definitions.Interfaces.Login
{
    public interface ILoginService
    {
        User Login(UserLogin userToLogin);
    }
}
