using System;
//using Microsoft.IdentityModel.Tokens;

using App.View.Models;
using App.Definitions.Entity;
using App.Definitions.Interfaces.DAOCommands;
using App.Logic.Security;

namespace App.Logic.Services.Entities
{
    internal class ModuleService
    {
        private readonly IModuleCommandsDAO _moduleCommandsDao;
        private readonly string _token;

        internal ModuleService(UserAccess user, IModuleCommandsDAO moduleCommandsDao)
        {

            // 🧪 NOTA DE LABORATORIO PERSONAL:
            // Sé que acoplar 'UserAccess' aquí rompe SOLID (Inversión de Dependencias) y POO (depender de clases concretas).
            // En un entorno profesional, esta validación y el manejo de tokens se delegarían 
            // al framework nativo mediante un Middleware, Claims y un IHttpContextAccessor (Inyección de Dependencias) (falta por aprender).
            if (user.CheckUserPermission("ModuleService") <= 0)
                throw new UnauthorizedAccessException("User has no permissions to this module.");

            //if(user == null) 
            //    throw new ArgumentNullException("NUser");

            _token = user.UserId.ToString(); // temporal token, just for testing pourposes.

            _moduleCommandsDao = moduleCommandsDao;
        }

        public long Register(ModuleDTO dto)
        {
            Module module = new Module(dto.Id, dto.Name, dto.ModuleName, dto.Observation);

            long lid = _moduleCommandsDao.Insert(module, _token);

            return lid;
        }
    }
}
