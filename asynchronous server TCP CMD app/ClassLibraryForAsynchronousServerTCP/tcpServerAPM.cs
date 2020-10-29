using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public class TcpServerAPM : TcpServer
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);
        public TcpServerAPM(IPAddress ip, int port) : base(ip, port) { }

        public override void Start()
        {
            StartListening();

            AcceptClient();
            Console.WriteLine("Klient został połączony");
        }


        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                NetworkStream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDataDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDataDelegate.BeginInvoke(NetworkStream, TransmissionCallback, tcpClient);
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            Console.WriteLine("Forced shutdown!\nCleaning...");
        }


        protected override void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[BufferSize];
            while (true)
            {
                try
                {
                    int message_size = stream.Read(buffer, 0, BufferSize);
                    stream.Write(buffer, 0, BufferSize);
                }
                catch (IOException e)
                {
                    break;
                }
            }
        }
    }
}
