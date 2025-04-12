using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

using DAOLib;
using Logic.Model;
using IDAO.Interfaces;
using IDAO.Enums;
using System.Collections.Generic;

namespace ServerSocketLib
{
    public class Server
    {
        private Thread _listenThread;
        private TcpListener _tcpListener;
        private Licenses _licenses;

        public Server() 
        {
            this._licenses = new Licenses();
            this._tcpListener = new TcpListener(IPAddress.Any, 9481);
            this._listenThread = new Thread(new ThreadStart(ServerStart));
            this._listenThread.Start();
        }

        private void ServerStart() 
        {
            this._tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this._tcpListener.AcceptTcpClient();

                //create a thread to handle communication with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(CommunicationHandler));
                clientThread.Start(client);
            }
        }

        private void CommunicationHandler(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            Byte[] data = new Byte[256];
            Byte[] action = Encoding.UTF8.GetBytes("Invalid license#EOF");

            String responseData = String.Empty;
            Int32 bytes = clientStream.Read(data, 0, data.Length);

            try 
            {
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                if (this._licenses.CheckLicense(responseData))
                {
                    License activeLicense = this._licenses.GetLicenseDetails(responseData);


                    // La Licencia está activa, notificar al cliente
                    action = Encoding.UTF8.GetBytes(DateTime.Now.ToShortDateString() + "true#EOF");
                    clientStream.Write(action, 0, action.Length);
                    clientStream.Flush();

                    this.DisplayConsole("[Socket: TPV License] " + responseData);

                    // Recepcion o envio de datos.
                    responseData = String.Empty;
                    data = new Byte[256];
                    bytes = clientStream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    if (responseData.Contains("Data"))
                        action = this.ReceiveData(ref clientStream, activeLicense);

                    else if (responseData.Contains("Banners"))
                        action = this.SendBanners(ref clientStream);
                }

                // Enviar Resultado de la operación de registro.
                clientStream.Write(action, 0, action.Length);
                this.DisplayConsole(ASCIIEncoding.UTF8.GetString(action, 0, action.Length));
            }
            catch
            {
                this.DisplayConsole("[Socket] Internal Error");
            } 
            finally
            {
                tcpClient.Close();
            }
        }

        // Recibir Objeto Serializado
        private byte[] ReceiveData(ref NetworkStream clientStream, License ActiveLicense)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] action = Encoding.UTF8.GetBytes("Error#EOF");

            try
            {
                // Actualizar objeto deserializado.
                IDbClass obj = (IDbClass)formatter.Deserialize(clientStream);

                this.DisplayConsole("Base de datos a escribir: tpv_" + ActiveLicense.CIF);

                if (this.UpdateServerInfo(obj, "tpv_" + ActiveLicense.CIF))
                    action = Encoding.UTF8.GetBytes("Updated#EOF");
            }
            catch (Exception ex)
            {
                action = Encoding.UTF8.GetBytes(ex.Message.Substring(0, ex.Message.Length < 240 ? ex.Message.Length : 240) + "Error#EOF");
            }

            return action;
        }

        private byte[] SendBanners(ref NetworkStream clientStream)
        {
            byte[] action = Encoding.UTF8.GetBytes("BannersList#EOF");

            List<Banner> banners = new DAO<Banner>().FindAll();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(clientStream, banners);
            
            return action;
        }

        private bool UpdateServerInfo(IDbClass data, String DatabaseName) 
        {
            bool ok = false;

            DatabaseActions dba = DatabaseActions.Update;
            switch (data.GetDataStatus()) 
            {
                case 0:
                case 1:
                    dba = DatabaseActions.Insert;
                    break;
                case 2:
                    dba = DatabaseActions.Update;
                    break;
                case 3:
                    dba = DatabaseActions.Delete;
                    break;
            }

            data.SetDataStatus(9);
            switch (data.GetType().ToString()) 
            {
                case "Logic.Model.User":
                    User user = (User)data;
                    ok = new DAO<User>(DatabaseConnection.Remote, DatabaseName).DataActions(user, dba) > 0;
                    break;
                case "Logic.Model.Payment":
                    Payment payment = (Payment)data;
                    ok = new DAO<Payment>(DatabaseConnection.Remote, DatabaseName).DataActions(payment, dba) > 0;
                    break;
                case "Logic.Model.Notification":
                    Notification not = (Notification)data;
                    ok = new DAO<Notification>(DatabaseConnection.Remote, DatabaseName).DataActions(not, dba) > 0;
                    break;
                case "Logic.Model.Zone":
                    Zone zone = (Zone)data;
                    ok = new DAO<Zone>(DatabaseConnection.Remote, DatabaseName).DataActions(zone, dba) > 0;
                    break;
                case "Logic.Model.Provider":
                    Provider provider = (Provider)data;
                    ok = new DAO<Provider>(DatabaseConnection.Remote, DatabaseName).DataActions(provider, dba) > 0;
                    break;
                case "Logic.Model.Customer":
                    Customer customer = (Customer)data;
                    ok = new DAO<Customer>(DatabaseConnection.Remote, DatabaseName).DataActions(customer, dba) > 0;
                    break;
                case "Logic.Model.Category":
                    Category category = (Category)data;
                    ok = new DAO<Category>(DatabaseConnection.Remote, DatabaseName).DataActions(category, dba) > 0;
                    break;
                case "Logic.Model.CashBox":
                    CashBox cashbox = (CashBox)data;
                    ok = new DAO<CashBox>(DatabaseConnection.Remote, DatabaseName).DataActions(cashbox, dba) > 0;
                    break;
                case "Logic.Model.ProvidersPayments":
                    ProvidersPayments providerp = (ProvidersPayments)data;
                    ok = new DAO<ProvidersPayments>(DatabaseConnection.Remote, DatabaseName).DataActions(providerp, dba) > 0;
                    break;
                case "Logic.Model.Item":
                    Item item = (Item)data;
                    ok = new DAO<Item>(DatabaseConnection.Remote, DatabaseName).DataActions(item, dba) > 0;
                    break;
                case "Logic.Model.Table":
                    Table table = (Table)data;
                    ok = new DAO<Table>(DatabaseConnection.Remote, DatabaseName).DataActions(table, dba) > 0;
                    break;
                case "Logic.Model.Ticket":
                    Ticket ticket = (Ticket)data;
                    ok = new DAO<Ticket>(DatabaseConnection.Remote, DatabaseName).DataActions(ticket, dba) > 0;
                    break;
                case "Logic.Model.TicketLine":
                    TicketLine ticketLine = (TicketLine)data;
                    ok = new DAO<TicketLine>(DatabaseConnection.Remote, DatabaseName).DataActions(ticketLine, dba) > 0;
                    break;
            }

            this.DisplayConsole("[Socket] " + data.GetType().ToString());
            this.DisplayConsole("[Socket] Dato 'ID':" + data.GetId() + "\r Dato 'name': " + data.GetName() + "\r DataStatus:" + data.GetDataStatus() + "\r");
            this.DisplayConsole("[Socket] Records affected: " + ok + "\r \r");

            return ok;
        }

        private void DisplayConsole(String txt) {
            Console.WriteLine(txt);
        }
    }
}
