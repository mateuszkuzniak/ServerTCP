using MessageLibrary;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerLibrary
{
    public class ServerTAP<T> : Server<T> where T : CommunicationProtocol, new()
    {
        public ServerTAP(IPAddress IP, int port, TextBox textBox, TextBox usersList) : base(IP, port, textBox, usersList) { }
        protected override void AcceptClient()
        {
            try
            {
                while (running)
                {
                    TcpClient client = TcpListener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    connectClientServer(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                    Task.Run(() => BeginDataTransmission(stream));
                    
                }
        }
            catch (InvalidOperationException ex)
            {
                error("ServerTAP.AcceptClient",ex.Message);
            }
}

        public override void Start()
        {
            running = true;
            StartListening();
            Task.Run(() => AcceptClient());
        }

    }
}
