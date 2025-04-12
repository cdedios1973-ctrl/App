// See https://aka.ms/new-console-template for more information
namespace App.Console
{
    using System;
    using System.Text;
    using App.Config;
    using App.Enums;
    using App.Events;
    using App.Logic;
    using App.Logic.IO.Handler;

    public class Program
    {
        //TODO: Add support to select database as xml)
        private const string MESSAGE_SENDER = "Program";
        private static AppMenuHandler _appMenuHandler;

        private static bool consoleLogin()
        {
            bool ok;
            string input = Console.ReadLine() + "";
            try
            {
                if (input.Contains(','))
                    _appMenuHandler.DoLogin(input);
                else
                    _appMenuHandler.Input(input);

                ok = AppMenuHandler.UserAuthenticated;
            }
            catch(Exception ex)
            {
                ok = false;
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message);
            }
            return ok;
        }

        private static void Init()
        {
            DataBaseType type = DataBaseType.SqLite;
            DisplayMessage.Displaytype = DisplaType.Console;

            AppInitialize init = new AppInitialize(type, DisplaType.Console);

            AppInitialize.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;
            DisplayMessage.messageHandler.UpdateStatus += MessageHandler_UpdateStatus;

            if (init.DDBBisInstalled == false)
                type = init.Initialize();

            if (type > DataBaseType.SqLite)
                throw new Exception("Database type not allowed to enter application");

            _appMenuHandler = new AppMenuHandler(new UserAccess(type, DisplaType.Console));
        }

        static void Main(string[] args)
        {
            try
            {
                Init();

                _appMenuHandler.Input("M");

                while (true)
                    if (consoleLogin() == true)
                        break;

                if (AppMenuHandler.UserAuthenticated == true)
                    _appMenuHandler.Input("M");
                    while (true)
                        _appMenuHandler.Input(Console.ReadLine().Trim());
            }
            catch (Exception ex)
            {
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message.ToString() + "\r\n" + " Press any key to exit. ");
                Console.ReadKey();
            }

            DisplayMessage.messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
            AppInitialize.messageHandler.UpdateStatus -= MessageHandler_UpdateStatus;
        }

        private static void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            UpdateStatusEventArgs ev = (UpdateStatusEventArgs)e;
            /*
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0,12}|", ev.Sender));
            sb.Append(string.Format("{0,40}|", ev.Message));
            sb.Append(string.Format("{0,10}|", ev.Status));
            sb.Append(string.Format("{0,20}|", ev.TimeReached));
            Console.WriteLine(">" + sb.ToString() + "<");
            */
            string output = string.Format("{0}| {1}| {2}| {3}",
                    ev.Sender.PadLeft(12),
                    ev.Message.PadRight(40),
                    ev.Status.PadRight(10),
                    ev.TimeReached.ToShortDateString().ToString().PadLeft(10));
            Console.WriteLine(output);
        }
    }
}





