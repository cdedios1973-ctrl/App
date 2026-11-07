using App.Models;

using System.Collections.ObjectModel;

namespace AppTasks.Views;

/*
public class Item
{
    public string Description { get; set; }
    public string ButtonName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

}
*/
public class ViewTestUsers_old
{
    public ObservableCollection<TestUser> Items { get; set; }

    public ViewTestUsers_old()
    {
        Items = new ObservableCollection<TestUser>();

        Items.Add(new TestUser("[Test] Log as Admin", "btnLog01", "Admin", "123"));
        Items.Add(new TestUser("[Test] Log as User 2", "btnLog02", "user 2", "123"));
        Items.Add(new TestUser("[Test] Log as User 3", "btnLog03", "user 3", "123"));
        Items.Add(new TestUser("[Test] Log as User 4", "btnLog04", "user 4", "123"));
        Items.Add(new TestUser("[Test] Log as User 5", "btnLog05", "user 5", "123"));

    }

    public void AddRow(TestUser item)
    {
        this.Items.Add(item);
    }


}