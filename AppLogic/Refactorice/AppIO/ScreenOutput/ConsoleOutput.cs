namespace App.Logic.IO
{
    using System.Reflection;

    using App.Models;
    using App.Enums;
    using App.Interfaces;
    using App.DAO;
    using AppLogic.Services.Input;

    internal static class ConsoleOutput
    {
        internal const string DISPLAY_ERROR = "Output display not implemented for :";
        internal const string DISPLAY_WRONG_OUTPUT = "Output display dosn't macht, config error";

        //TODO: Apply filters to data 'also pagination' (users like... permissions like... etc)

        #region Methods: Draw && Flush && Clear

        private static void Flush()
        {
            AppOutputHandler._dsp.UserName = "";
            AppOutputHandler._dsp.Title = "";
            AppOutputHandler._dsp.ColumnsName = "";
            AppOutputHandler._dsp.DataRows = "";
        }

        private static void Draw()
        {
            Console.WriteLine(AppOutputHandler._dsp.UserName);
            Console.WriteLine(AppOutputHandler._dsp.Title);
            Console.WriteLine(AppOutputHandler._dsp.ColumnsName);
            Console.WriteLine(AppOutputHandler._dsp.DataRows);
            
            if(AppInputService.UserAuthenticated == true)
                Console.WriteLine(AppOutputHandler._dsp.DDBBActions);
            
            Console.WriteLine(AppOutputHandler._dsp.MenuActions);
        }

        #endregion

        #region Methods: Console Display

        internal static void DrawConsole<T>(ref DataFilter filter) where T : IModel
        {
            if (AppInputService.GetCurrentUser.Display != DisplaType.Console)
                throw new Exception(DISPLAY_WRONG_OUTPUT);

            UserAccess user = AppInputService.GetCurrentUser;
            AppOutputHandler._dsp.UserName = user.GetUserFullName;

            DrawConsoleDetail<T>(new DAOData<T>(user, ref filter).GetData);
            Draw();
            Flush();
        }

        internal static void DrawConsole<T>(DrawAppMenu menu, bool vertical) where T : IModel
        {
            if (AppInputService.GetCurrentUser.Display != DisplaType.Console)
                throw new Exception(DISPLAY_WRONG_OUTPUT);

            AppCommand cmd = new AppCommand();

            AppOutputHandler._dsp.Title = "Welcome user, input action.";
            AppOutputHandler._dsp.ColumnsName = DrawConsoleMenu(true, cmd.GetHeaderList);

            switch (menu)
            {
                case DrawAppMenu.All:
                    break;
                case DrawAppMenu.LogIn:
                    AppOutputHandler._dsp.DataRows = DrawConsoleMenu(true, cmd.GetCommandList) + Environment.NewLine;
                    AppOutputHandler._dsp.DataRows += DrawConsoleMenu(true, cmd.GetLoginCommandList);
                    break;
                case DrawAppMenu.Commands:
                    AppOutputHandler._dsp.DataRows = DrawConsoleMenu(vertical, cmd.GetCommandList);
                    break;
                case DrawAppMenu.DDBB:
                    AppOutputHandler._dsp.DataRows = DrawConsoleMenu(vertical, cmd.GetDDBBCommandList);
                    break;
                case DrawAppMenu.Users:
                    AppOutputHandler._dsp.DataRows = DrawConsoleMenu(vertical, cmd.GetUsersList);
                    break;
                case DrawAppMenu.Help:
                    AppOutputHandler._dsp.DataRows = DrawConsoleMenu(true, cmd.GetCommandList) + Environment.NewLine;
                    AppOutputHandler._dsp.DataRows += DrawConsoleMenu(true, cmd.GetDDBBCommandList) + Environment.NewLine;
                    AppOutputHandler._dsp.DataRows += DrawConsoleMenu(true, cmd.GetUsersList);
                    break;
            }
            if (menu != DrawAppMenu.LogIn && AppInputService.GetCurrentUser.GetUser == null)
                AppOutputHandler._dsp.DataRows += DrawConsoleMenu(true, cmd.GetLoginCommandList);

            Draw();
            Flush();
        }

        private static void DrawConsoleDetail<T>(List<T> data) where T : IModel
        {
            UserAccess user = AppInputService.GetCurrentUser;
            ColumnsConfig cols = new DAOSchemas<T>(ref user).GetColumnsConfig;
            IModel model = (IModel)Activator.CreateInstance(typeof(T));

            AppOutputHandler._dsp.Title         = model.IGetTableName();
            AppOutputHandler._dsp.ColumnsName   = cols.Header;

            string detail = "";

            if (data.Count > 0)
                foreach (T t in data)
                {
                    PropertyInfo[] props = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in props)
                    {
                        detail += (pi.GetValue(t, null) + " | ").PadLeft(cols.Column[pi.Name].Value2);
                    }
                    detail += Environment.NewLine;
                }
            else
                detail = ".No " + AppOutputHandler._dsp.Title + " found.";

            AppOutputHandler._dsp.DataRows = detail;
        }

        private static string DrawConsoleMenu(bool vertical, List<AppCommand> lst)
        {
            string msg = "";
            foreach (AppCommand cmd in lst)
            {
                //msg += cmd.Command + " " + cmd.Name + " " + cmd.Description + Environment.NewLine;
                if (vertical)
                    msg += cmd.Name + " " + cmd.Description + Environment.NewLine;
                else
                    msg += cmd.Name + " | ";
            }
            return msg;
        }

        #endregion

    }
}
