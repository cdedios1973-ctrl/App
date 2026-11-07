using System;
using App.Config.DDBB;
using App.Enums;
using App.Logic;
using App.Models;
using AppTasks.Controls;
using AppTasks.Views;

namespace AppTasks.Controller.Layers
{
    internal sealed class LayerInstall : LayerMaster
    {
        private static ViewInstall _installGrid = new ViewInstall();

        #region Constructors

        internal LayerInstall(ContentPage page) : base(page)
        { }

        internal LayerInstall(ContentPage page, EventHandler eventHandler) : base(page, eventHandler)
        { }

        #endregion

        #region Action : Show menu

        protected internal override void Show()
        {
            if (MainPage.AppDbExist == true)
            {
                AppLayers.AddComponent(base._page, "layerMaster", new AppConfirmation(), base._event);
                AppLayers.SetLayoutLabelValue(base._page, "lPage", "lblStatus", "Atention! Database already exist.");
            }
            //AppLayers.AddComponent(_page, "layerSubmenu", new ViewTitle("Install database"), _event);
            AppLayers.AddComponent(base._page, "layerDetail", _installGrid, base._event);
        }

        #endregion

        #region Action : Install

        protected internal bool Action(ref AppInitialize appInit, EventHandler ev,  bool reInstall)
        {
            try
            {
                // TODO : back to enable  Disabled layer child(confirmation) when finmished process
                AppLayers.DisableComponent(base._page, "layerMaster");

                if (reInstall == true || MainPage.AppDbExist == false)
                {
                    AppInitialize.messageHandler.UpdateStatus += ev;

                    if (appInit == null)
                        appInit = new(DataBaseType.SqLite, DisplaType.Mobile);

                    DataBaseType tmp = appInit.Initialize();
                    if (tmp != DataBaseType.SqLite)
                        DisplayMessage.Show(MainPage.MESSAGE_SENDER, MainPage.APP_INSTALL_ERROR);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.Show(MainPage.MESSAGE_SENDER, ex.Message);
            }
            finally
            {
                AppInitialize.messageHandler.UpdateStatus -= ev;
                MainPage.AppDbExist = appInit.DDBBisInstalled;
                appInit = null;

            }
            return reInstall;
        }

        #endregion

        #region Action : Insert data to grid

        protected internal void Action(MessageEvent mEvent)
        {
            _installGrid.AddRow(mEvent);
        }

        #endregion

    }
}
