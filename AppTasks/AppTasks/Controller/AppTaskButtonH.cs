using App.Enums;
using App.Logic.IO.Handler;
using App.Logic;
using AppTasks.Resources;
using AppTasks.Views;
using System;

namespace AppTasks.Controller
{
    public class AppTaskButtonH
    {
        private ContentPage _page;
        private EventHandler _eventHandler;

        public AppTaskButtonH(ContentPage page, EventHandler eventHandler)
        {
            this._page = page;
            this._eventHandler = eventHandler;
        }

        #region Action : Choice No

        public void ActionResponseNo()
        {
            AppLayers.AddComponent(_page, "layerMaster", new Welcome(), _eventHandler);
            AppLayers.RemoveComponent(_page, "layerSubmenu", "layerSubmenu", _eventHandler);
        }

        #endregion

        #region Action : Show Login menu && Action : Logout 

        public void ActionLogin(ref AppMenuHandler appMenu, Button btn)
        {
            if (btn.Text == MainPage.APP_LOGIN)
            {
                btn.IsEnabled = false;
                AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
                AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _eventHandler);
                AppLayers.AddComponent(_page, "layerMaster", new AppLogin(), _eventHandler);
            }
            else
                appMenu.Input("L");
        }

        #endregion

        #region Action : Auto - Login as Admin or User

        public bool ActionLoginAs(ref AppMenuHandler appMenu, string input)
        {
            if (appMenu == null)
                appMenu = new AppMenuHandler(new UserAccess(DataBaseType.SqLite, DisplaType.Mobile));

            return appMenu.DoLogin((input == "btnLog01" ? "admin" : "user 2") + ",123");
        }

        #endregion

        #region Action : Install Database

        public void ActionInstall(bool dbExist)
        {
            if (dbExist == true)
                AppLayers.AddComponent(_page, "layerMaster", new AppConfirmation(), _eventHandler);

            AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
            AppLayers.AddComponent(_page, "layerDetail", new AppInstall(), _eventHandler);
        }

        #endregion

        #region Action : Back to main menu

        public void ActionBack()
        {
            AppLayers.AddComponent(_page, "layerMaster", new Welcome(), _eventHandler);
            AppLayers.RemoveComponent(_page, "layerSubmenu", "layerSubmenu", _eventHandler);
        }

        #endregion

        #region Action : Show Options menu

        public void ActionOptions(ref Button btn)
        {
            bool hasItem = AppLayers.SearchVLayoutChildrenByName(_page, "layerSubmenu");
            if (hasItem == false)
                AppLayers.AddComponent(_page, "layerSubmenu", new AppMenu(), _eventHandler);
            else
                AppLayers.RemoveComponent(_page, "layerSubmenu", "layerSubmenu", _eventHandler);
            btn.IsEnabled = hasItem;
        }

        #endregion

        #region Action : Remove layers to show Users List.

        public void ActionUsers()
        {
            AppLayers.RemoveComponent(_page, "layerButtons", "layerButtons", _eventHandler);
            AppLayers.RemoveComponent(_page, "layerMaster", "layerMaster", _eventHandler);
            AppLayers.RemoveComponent(_page, "layerDetail", "layerDetail", _eventHandler);
        }

        #endregion

    }
}
