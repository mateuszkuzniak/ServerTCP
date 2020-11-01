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

        /// <summary>
        /// Funkcja startująca server TCP
        /// </summary>
        public override void Start()
        {
            StartListening();
            AcceptClient();
            Console.WriteLine("Client is logged");
        }

        /// <summary>
        /// Funckja akceptująca klienta
        /// </summary>
        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDataDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDataDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);
            }
        }

        /// <summary>
        /// Funkcja czyszcząca
        /// </summary>
        /// <param name="ar"></param>
        private void TransmissionCallback(IAsyncResult ar)
        {
            Console.WriteLine("Connection lost!\nCleaning...");
        }


        /// <summary>
        /// Pierwszy kontak z klientem -> MENU
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private int userMenu(NetworkStream stream)
        {
            int choose;
            string temp;

            while (true)
            {
                WriteMessage(stream, Message.helloMessage);
                temp = ReadMessage(stream);
                if (int.TryParse(temp, out choose))
                {
                    if ((choose == 1) || (choose == 2))
                    {
                        break;
                    }
                    else
                        stream.Write(Message.menuError, 0, Message.menuError.Length);
                }
                else
                    stream.Write(Message.invalidChar, 0, Message.invalidChar.Length);
            }

            return choose;
        }


        /// <summary>
        /// Funkcja odpowiedzialan za delegat transmisji
        /// </summary>
        /// <param name="stream"></param>
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            Account user = new Account();
            while (true)
            {
                try
                {
                    if ((int)userMenu(stream) == 1)
                        Console.WriteLine("1");
                    else 
                        Console.WriteLine("2");

                }
                catch (IOException)
                {
                    break;
                }
            }
        }
    }
}
