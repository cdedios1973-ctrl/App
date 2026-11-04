// See https://aka.ms/new-console-template for more information

using App.Definitions.Events;
using App.Logic;
using AppConsole.CORE;

namespace AppConsole
{
    class Program
    {
        private const string MESSAGE_SENDER = "Program";

        static void Main(string[] args)
        {
            // TODO : need to create singleton of UserAccess to use at Logic.Security -> load once (permissions to each module)

            Console.WriteLine("Iniciando aplicación...");

            try
            {
                AplicationManager.manager.InitializeConfiguration();
                AplicationManager.manager.StartMainThread();

                _appMenuHandler.Input("M"); // TODO : Change to AppInputService at logic

                while (true)
                    _appMenuHandler.Input(Console.ReadLine().Trim()); // TODO : Change to AppInputService at logic

            }
            catch (Exception ex)
            {
                // La capa de presentación maneja la salida visual del error
                Console.ForegroundColor = ConsoleColor.Red;
                DisplayMessage.Show(MESSAGE_SENDER, ex.Message.ToString() + "\r\n" + " Press any key to exit. "); // TODO : change namespace of displaymessage
                Console.ReadKey();
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Cerrando el programa...");
                AplicationManager.manager.CloseMainThread();
                Console.ReadKey();
            }
        }

        internal static void MessageHandler_UpdateStatus(object? sender, EventArgs e)
        {
            UpdateStatusEventArgs ev = (UpdateStatusEventArgs)e;
            string output = string.Format("{0}| {1}| {2}| {3}",
                    ev.Sender.PadLeft(12),
                    ev.Message.PadRight(40),
                    ev.Status.PadRight(10),
                    ev.TimeReached.ToShortDateString().ToString().PadLeft(10));
            Console.WriteLine(output);
        }

    }
}




