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
      
        #region Message 
        readonly byte[] helloMessage = new ASCIIEncoding().GetBytes("Witaj! Zaloguj się do serwera. Podaj login:\n\r");
        #endregion


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
            Console.WriteLine("Connection lost!\nCleaning...");
        }


        protected override void BeginDataTransmission(NetworkStream stream)
        {
            Account user = new Account();
            byte[] reciveBuffer = new byte[BufferSize];
            while (true)
            {
                try
                {
                    stream.Write(helloMessage, 0, helloMessage.Length);

                    int message_size = stream.Read(reciveBuffer, 0, BufferSize);
                    stream.Write(reciveBuffer, 0, message_size);
                }
                catch (IOException)
                {
                    break;
                }
            }
        }
    }
}
