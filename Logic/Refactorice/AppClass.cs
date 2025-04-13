using System;

// TODO : Remove this class ASAP

namespace App.Models
{
    public static class AppClass
    {
        public static object GetClassByName(string className)
        {
            string name = "App.Models." + className;

            Type type = Type.GetType(name, false);
            object obj = Activator.CreateInstance(type);

            return obj;
        }
    }

}
