using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;
using ServerLibrary;

namespace CommProtocolLibrary
{
    public class TcpServerAPM : TcpServer
    {
        #region BASIC_FUNCTION
        private delegate void TransmissionDataDelegate(NetworkStream stream);
        public TcpServerAPM(IPAddress ip, int port) : base(ip, port) { }

        /// <summary>
        /// Funkcja startująca server TCP
        /// </summary>
        public override void Start()
        {
            StartListening();
            AcceptClient();
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

            Console.WriteLine(Message.connectionCloseCONNECTION);
        }
        #endregion

        #region MENU
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
                WriteMessage(stream, Message.helloMessageMENU);
                temp = ReadMessage(stream);
                if (int.TryParse(temp, out choose))
                {
                    if ((choose == 1) || (choose == 2))
                    {
                        break;
                    }
                    else
                        stream.Write(Message.menuErrorMENU, 0, Message.menuErrorMENU.Length);
                }
                else
                    stream.Write(Message.invalidCharErrorMENU, 0, Message.invalidCharErrorMENU.Length);
            }
            return choose;
        }
        #endregion

        #region LOGIN
        /// <summary>
        /// Logowanie użytkownika do systemu
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="user"></param>
        private void loginProgram(NetworkStream stream, ref Account user)
        {
            string userName;
            while (true)
            {
                WriteMessage(stream, Message.giveLoginLOGIN);
                userName = ReadMessage(stream).ToLower();
                if (database.checkUserExist(userName))
                {
                    break;
                }
                else if (userName == "break")
                {
                    break;
                }
                else
                {
                    WriteMessage(stream, Message.userDoesNotExistsErrorLOGIN);
                }
            }

            if (userName != "break")
            {
                user = database.getUserWithDatabase(userName);
                if (!user.IsLogged)
                {
                    string password;
                    while (true)
                    {
                        WriteMessage(stream, Message.givePasswordLOGIN);
                        password = ServerLibrary.HashAlgorithm.getHash( ReadMessage(stream));
                        if (password == user.Pass)
                        {
                            break;
                        }
                        else
                            WriteMessage(stream, Message.badPasswordErrorLOGIN);
                    }

                    database.updateLoginStatus(user);
                    WriteMessage(stream, Message.loggedIn(user.Login));
                }
                else
                {
                    user.Clear();
                    WriteMessage(stream, Message.userIsLoggedError);
                }
            }


        }

        /// <summary>
        /// Funkcja zmienia status użytkownika na false - wylogowany, jeżeli owy użytkownik widnieje w bazie jako zalogowany
        /// </summary>
        /// <param name="user"></param>
        void logOutUser(Account user)
        {
            if (user.IsLogged)
            {
                database.updateLoginStatus(user);
            }
        }
        #endregion

        #region REGISTRY
        private void RegistryProgram(NetworkStream stream)
        {
            string userName, password;
            while (true)
            {
                WriteMessage(stream, Message.giveUserNameREGISTRY);
                userName = ReadMessage(stream).ToLower();
                if (AdditionalFunctions.validUserName(userName))
                {
                    if (!database.checkUserExist(userName))
                    {
                        break;
                    }
                    else
                    {
                        WriteMessage(stream, Message.busyUserNameREGISTRY);
                    }
                }
                else if (userName == "break")
                    break;
                else
                    WriteMessage(stream, Message.invalidUserNameREGISTRY);
            }

            if (userName != "break")
            {
                Account user = new Account();
                user.Login = userName;

                while (true)
                {
                    WriteMessage(stream, Message.enterPasswordREGISTRY);
                    password = ReadMessage(stream);
                    if (AdditionalFunctions.securePassword(password))
                    {
                        user.Pass = ServerLibrary.HashAlgorithm.getHash(password);
                        break;
                    }
                    else
                    {
                        WriteMessage(stream, Message.invalidPasswordREGISTRY);
                    }
                }

                database.addUser(user.Login, user.Pass);
            }
        }
        #endregion

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
                    if (userMenu(stream) == 1)
                    {
                        loginProgram(stream, ref user);
                    }
                    else
                    {
                        RegistryProgram(stream);
                    }

                    if (user.IsLogged)
                    {
                        WriteMessage(stream, Message.logOut);
                        ReadMessage(stream);

                        logOutUser(user);
                        WriteMessage(stream, Message.userLogOut);

                    }
                }
                catch (IOException)
                {
                    logOutUser(user);
                    Console.WriteLine(Message.lostConnectionCONNECTION);
                    break;
                }
            }
        }
    }
}
