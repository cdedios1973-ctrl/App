using System;

using App.Definitions.Entity;

namespace App.Infrastructure.Persistencee.Interfaces;

public interface IModuleWriteRepository
{
    long Insert(Module module);
    long Update(Module module);
    bool Delete(Module module);

}