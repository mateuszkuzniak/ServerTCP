using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class ServerTAP<T> : Server<T> where T : CommunicationProtocol, new()
    {
        public ServerTAP(IPAddress IP, int port) : base(IP, port) { }
        protected override void AcceptClient()
        {
            try
            {
                while (true)
                {
                    TcpClient client = TcpListener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    Task.Run(() => BeginDataTransmission(stream));
                }
        }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
}

        public override void Start()
        { 
            Task.Run(() => ListeningTask());
            //ServerConsole();
        }

        protected override void ListeningTask()
        {
            running = true;
            StartListening();
            AcceptClient();
        }
    }
}
