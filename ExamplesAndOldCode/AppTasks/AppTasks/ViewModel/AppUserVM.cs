using System.Collections.ObjectModel;
using System.Windows.Input;

using App.Models;

namespace AppTasks.ViewModels
{
    public class AppUserVM
    {
        public ObservableCollection<Users> AppUsers { get; set; }

        public ICommand ApplicationQuit { get; private set; }
        public ICommand ApplicationOptions { get; private set; }

        public AppUserVM()
        {
            ApplicationQuit = new Command((name) =>
            {
                Application.Current.Quit();
            });


            AppUsers = new ObservableCollection<Users>
            {
                new Users { Id = 0, Name = "No user", Surname = "Log in", Address = "", CountryIdentity = "" }
            };
        }
    }
}
