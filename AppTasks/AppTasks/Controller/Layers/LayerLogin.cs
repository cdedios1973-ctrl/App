using System;
using AppTasks.Views.Login;

using App.Enums;
using App.Logic;
using App.Logic.IO.Handler;
using App.DAO;

namespace AppTasks.Controller.Layers
{
    internal sealed class LayerLogin : LayerMaster
    {
        private static VerticalStackLayout _vLayout;
        private static ViewLogin _viewLogin;

        #region Constructors

        internal LayerLogin(ContentPage page) : base(page)
        {
            _vLayout = (VerticalStackLayout)_page.FindByName("layerMaster");
            if (_vLayout == null)
                throw new Exception(APP_ERROR_FIND_OBJECT + "layerMaster");
        }

        internal LayerLogin(ContentPage page, EventHandler eventHandler) : base(page, eventHandler)
        { }

        #endregion

        #region Action : Show Login menu

        protected internal override void Show()
        {
            this.Destroy();
            if (_viewLogin == null)
                _viewLogin = new ViewLogin();

            AppLayers.AddComponent(_page, "layerMaster", _viewLogin, _event);
        }

        #endregion

        #region Action : Destroy

        protected internal override void Destroy()
        {
            _vLayout = null;
            _viewLogin = null;
            AppLayers.RemoveComponent(_page, "layerMaster", "layerMaster", _event);
        }

        #endregion

        #region Action : Login

        protected internal bool Action(ref AppMenuHandler appMenu)
        {
            string input = "";
            VerticalStackLayout vlayout = (VerticalStackLayout)_viewLogin.FindByName("layerLogin");
            input = AppLayers.GetEntryValue(vlayout, "txtUser") + ",";
            input += AppLayers.GetEntryValue(vlayout, "txtPass");

            UserAccess _user = new DAOLogin(input, DataBaseType.SqLite, DisplaType.Mobile).GetValidateUser;
            appMenu = new AppMenuHandler(_user);

            return _user.IsUserAuthenticated;
        }

        #endregion

    }
}
