using App.Models;
using System.Collections.ObjectModel;

namespace AppTasks.Views.TestUsers;

public partial class ViewTUser : ContentView
{
    public ObservableCollection<TestUser> testUsers { get; set; }

    public ViewTUser()
	{
		InitializeComponent();

        testUsers = new ObservableCollection<TestUser>();

        testUsers.Add(new TestUser("[Test] Log as Admin", "btnLog01", "Admin", "123"));
        testUsers.Add(new TestUser("[Test] Log as User 2", "btnLog02", "user 2", "123"));
        testUsers.Add(new TestUser("[Test] Log as User 3", "btnLog03", "user 3", "123"));
        testUsers.Add(new TestUser("[Test] Log as User 4", "btnLog04", "user 4", "123"));
        testUsers.Add(new TestUser("[Test] Log as User 5", "btnLog05", "user 5", "123"));

        this.lTestUsers.ItemsSource = testUsers;
        this.lTestUsers.BindingContext = this;
    }

}