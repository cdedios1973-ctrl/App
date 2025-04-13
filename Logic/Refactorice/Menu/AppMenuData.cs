namespace App.Logic.Input
{
    using System;
    using App.Models;

    public class AppMenuData
    {
        private static List<BasicMenu> _menu;
        private static UserAccess _user;

        public AppMenuData(ref UserAccess user)
        {
            _user = user;
            if (_menu == null)
                this.initializeMenu();
        }

        private void initializeMenu()
        {
            _menu = new List<BasicMenu>
            {
                new (MenuFilter.DDBB,    ""  , "     ", "----------------------------------------------------------------------------"),
                new (MenuFilter.DDBB,    "S" , "DDBB" , "Current Database Selection : { " + _user.DDBBType.ToString() + " } "),
                new (MenuFilter.DDBB,    ""  , "     ", "----------------------------------------------------------------------------"),
                new (MenuFilter.Users,   "U" , "USERS", "U (+Enter) - Show Users list (Test mode). "),
                new (MenuFilter.Options, "S" , "DDBB" , "S (+Enter) - Change Application Datasource."),
                new (MenuFilter.Options, "I" , "INIT" , "I (+Enter) - Restore the application to default (delete all data)."),
                new (MenuFilter.Options, "?" , "HELP" , "? (+Enter) - Show Application Help."),
                new (MenuFilter.Menu,    "Q" , "QUIT" , "Close the application."),
                new (MenuFilter.Menu,    "M" , "MENU" , "| ? Help | U Users | S Change Source | I Initialize | O Options | Q Quit |"),
                new (MenuFilter.Login,   ""  , ""     , "----------------------------------------------------------------------------"),
                new (MenuFilter.Login,   ""  , ""     , "Insert user name (comma) password, q to quit. Example: myUsername,myPassword"),
                new (MenuFilter.Login,   ""  , ""     , "----------------------------------------------------------------------------"),
                new BasicMenu()
            };
        }

        public List<BasicMenu> GetMenuHandler
        {
            get 
            {
                if (_menu == null)
                    this.initializeMenu();
                return _menu;
            }
        }

        public void GetMenuWindow(BasicMenu.MenuFilter menu)
        {
            List<BasicMenu> result = _menu;
            if (menu != BasicMenu.MenuFilter.All)
                result = _menu.FindAll(s => s.Menufilter.Equals(menu));

            foreach (BasicMenu mnu in result)
                Console.WriteLine(mnu.Name, "\r\n");
        }

    }
}
