using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class ServerTAP<T> : Server<T> where T: CommunicationProtocol, new()
    {
        public ServerTAP(IPAddress IP, int port) : base(IP, port) { }
        protected override void AcceptClient()
        {
            while (true)
            {

                    TcpClient client = TcpListener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    Task.Run(() => BeginDataTransmission(stream));
                

            }
        }

        public override void Start()
        {

            running = true;
            StartListening();
            AcceptClient();
        }
    }
}
