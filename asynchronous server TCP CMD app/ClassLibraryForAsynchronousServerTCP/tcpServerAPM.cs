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
                    stream.Write(Message.invalidCharError, 0, Message.invalidCharError.Length);
            }
            return choose;
        }


        private void loginProgram(NetworkStream stream, ref Account user)
        {
            string userName;
            while(true)
            {
                WriteMessage(stream, Message.giveLogin);
                userName = ReadMessage(stream);
                if (database.checkUserExist(userName))
                {
                    break;
                }
                else
                {
                    WriteMessage(stream, Message.userDoesNotExistsError);
                }
            }

            user = database.getUserWithDatabase(userName);
            if (!user.IsLogged)
            {
                string password;
                while (true)
                {
                    WriteMessage(stream, Message.givePassword);
                    password = ReadMessage(stream);
                    if (password == user.Pass)
                        break;
                    else
                        WriteMessage(stream, Message.badPasswordError);
                }

                database.updateLoginStatus(user);
                WriteMessage(stream, Message.loggedIn(user.Login));
            }
            else
                WriteMessage(stream, Message.userIsLoggedError);
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
                    if (userMenu(stream) == 1) //Logowanie
                    {
                        loginProgram(stream, ref user);
                    }
                    else  //Rejstracja 
                        Console.WriteLine("2");
                }
                catch (IOException)
                {

                    if (user.IsLogged == true)
                    {
                        database.updateLoginStatus(user);
                        Console.WriteLine(Message.lostConnection);
                    }
                    break;
                }
            }
        }
    }
}
