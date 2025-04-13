using System.Collections.ObjectModel;
using System.Windows.Input;

using AppTasks.Models;

namespace AppTasks.ViewModels
{
    public class MenuViewModel
    {
        public ObservableCollection<MainMenu> AppMenus { get; set; }

        public MenuViewModel()
        {
            AppMenus =
            [
                new() { LId = "btnInstall", Text = "Install", Hint = "" },
                new() { LId = "btnSource", Text = "Source", Hint = "" },
                new() { LId = "btnUsers", Text = "Users", Hint = "" },
                new() { LId = "btnLogin", Text = "Login", Hint = "" }
            ];
        }
    }

}
