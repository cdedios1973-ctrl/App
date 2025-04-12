namespace App.Models
{
    using System;

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

        //test<IModel>(model);
        /*
        private T test<T>(T data) where T : IModel
        {
            Type type = data.GetType();
            object obj = Activator.CreateInstance(type);
            Console.WriteLine("JOIN  : " + data.getSQLJoin());
            Console.WriteLine("Class : " + obj.GetType().ToString());
            return (T)obj;
        }

        Dictionary<string, Type> _classes = new Dictionary<string, Type>() {
                { "Users", typeof(User)},
                { "UsersPermission", typeof(UsersPermission)},
                { "UsersLogin", typeof(UsersLogin)},
                { "Permissions", typeof(Permissions)},
                { "Tasks", typeof(Tasks)},
                { "UserTasks", typeof(UserTasks)} };

                public T Create<T>(T a) where T : IModel
                {
                    return a;
                }
        */

}
