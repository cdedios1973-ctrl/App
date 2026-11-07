namespace AppTasks.Views
{
    public partial class MenuView : ContentView
    {
        public delegate void OnMenuViewButtonClickedDelegate(object sender, EventArgs e);
        public OnMenuViewButtonClickedDelegate OnMenuViewButtonClicked { get; set; }

        public MenuView()
        {
            InitializeComponent();
        }

        private void MenuViewButtonClicked(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            string asgf = btn.StyleId;

            if (OnMenuViewButtonClicked != null)
                OnMenuViewButtonClicked(sender, e);

        }
    }
}
