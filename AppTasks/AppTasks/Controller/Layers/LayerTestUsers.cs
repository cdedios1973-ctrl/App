using System;

using AppTasks.Views;
using AppTasks.Views.TestUsers;
using AppTasks.Views.Login;
using App.Logic.IO.Handler;

namespace AppTasks.Controller.Layers
{
    internal sealed class LayerTestUsers : LayerMaster
    {
        private static LayerTestUsers _instance;

        //public static ObservableCollection<TestUser> _testUsers = new ObservableCollection<TestUser>();

        #region Constructors
        /*
        public LayerTestUsers() 
        { }

        private LayerTestUsers() //: base(page, eventHandler)
        { }

        internal LayerTestUsers(ContentPage page) : base(page)
        { }
        */
        internal LayerTestUsers(ContentPage page, EventHandler eventHandler) : base(page, eventHandler)
        { }

        #endregion

        #region Singleton pattern

        /*
        public static LayerTestUsers GetInstance()
        {
            if (_empty == null)
            {
                _empty = new LayerTestUsers();
            }
            return _empty;
        }
        */        
        public static LayerTestUsers GetInstance(ContentPage page, EventHandler eventHandler)
        {
            if (_instance == null)
            {
                _instance = new LayerTestUsers(page, eventHandler);
            }
            return _instance;
        }

        #endregion

        #region Action : Show test users menu

        protected internal void Show(string action)
        {
            //AppLayers.AddComponent(base._page, "layerMaster", new ViewTestUsers(), base._event);
            //AppLayers.AddComponent(base._page, "layerDetail", new ViewLogin(), base._event);
            AppLayers.RemoveComponent(base._page, "layerActions", "layerActions", base._event);
            AppLayers.RemoveComponent(base._page, "layerMaster", "layerMaster", base._event);

            // TODO: change only title, do not change buttons.
            //AppLayers.AddComponent(base._page, "layerSubmenu", new ViewTitle("Test Users list"), base._event);
            AppLayers.AddComponentB(base._page, "layerDetail", new ViewLogin(), base._event);

            //AppLayers.AddComponent(_page, "layerMaster", new ViewTUser(), _event);

            ViewTUser vUser;

            VerticalStackLayout vLayout = AppLayers.GetVerticalLayoutByName(base._page, "layerMaster");
            if (vLayout != null)
            {
                vUser = new ViewTUser();

                //vLayout.Add(vUser);
                vLayout.Children.Add(vUser);

                ListView lView = AppLayers.GetListViewByName(vUser, "vTestUser", "lTestUsers");



                //lView.ItemAppearing += LView_ItemAppearing;
                //lView.ItemDisappearing += LView_ItemDisappearing;
                /*
                Grid grd = AppLayers.GetGridByName(lView, "grdItems");
                if (grd != null)
                {
                    string asdf = grd.StyleId;
                }
                */

            }

        }
        /*
         * 
         * Occurs when scrolling (for example)
        private void LView_ItemDisappearing(object? sender, ItemVisibilityEventArgs e)
        {
            string asd = e.Item.ToString();
        }
        private void LView_ItemAppearing(object? sender, ItemVisibilityEventArgs e)
        {
            string asd = e.Item.ToString();
            if (e.Item is Button)
            {
                Button btn = (Button)e.Item;
                string name = btn.StyleId;
            }

        }
        */
        #endregion

        #region Action : Destroy

        protected internal override void Destroy()
        {
            AppLayers.RemoveComponent(base._page, "layerMaster", "layerMaster", base._event);
            AppLayers.RemoveComponent(base._page, "layerDetail", "layerDetail", base._event);
        }

        #endregion

        #region Action : Login

        protected internal bool Action(ref Button btn, ref AppMenuHandler appMenu)
        {
            string[] usr = btn.Text.Split(",");
            AppLayers.SetLayoutEntryValue(base._page, "layerDetail", "txtUser", usr[0]);
            AppLayers.SetLayoutEntryValue(base._page, "layerDetail", "txtPass", usr[1]);

            return new LayerLogin(base._page).Action(ref appMenu);
        }

        #endregion

    }
}
