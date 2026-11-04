using System;
using App.Logic.IO.Handler;
using AppTasks.Views.Menu;

namespace AppTasks.Controller.Layers
{
    internal sealed class LayerMenu : LayerMaster
    {
        private static LayerMenu _instance;
        private static ViewMenu _viewMenu;
        private static ViewUser _viewUser;

        #region Constructors

        internal LayerMenu(ContentPage page) : base(page)
        {
            /*
            _viewMenu = (ViewMenu)_page.FindByName("CurrentMenu");
            _viewUser = (ViewUser)_page.FindByName("CurrentUser");
            if (_viewMenu == null || _viewUser == null)
                throw new Exception(APP_ERROR_FIND_OBJECT + "ViewMenu - ViewUser");
            */
        }

        #endregion

        #region Singleton pattern

        public static LayerMenu GetInstance(ContentPage page)
        {
            if (_instance == null)
                _instance = new LayerMenu(page);
            return _instance;
        }

        #endregion

        #region Action : Show menu

        protected internal void Show(bool isLogged = true)
        {
            /*
            if(isLogged == false)
                MainPage._appMenuHandler.Input("L");

            _viewMenu.UserLogged = MainPage.GetUserName;
            _viewMenu.BindingContext = isLogged == true ? this : null;
            _viewUser.BindingContext = isLogged == true ? this : null;
            ActionBack();
            */
        }

        #endregion

    }
}
