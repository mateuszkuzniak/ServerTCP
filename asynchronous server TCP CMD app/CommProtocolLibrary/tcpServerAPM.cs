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
                if (_usersDatabase.checkUserExist(userName))
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
                user = _usersDatabase.getUserWithDatabase(userName);
                if (!user.IsLogged)
                {
                    string password;
                    while (true)
                    {
                        WriteMessage(stream, Message.givePasswordLOGIN);
                        password = ServerLibrary.HashAlgorithm.getHash(ReadMessage(stream));
                        if (password == user.Pass)
                        {
                            break;
                        }
                        else
                            WriteMessage(stream, Message.badPasswordErrorLOGIN);
                    }

                    _usersDatabase.updateLoginStatus(user);
                    WriteMessage(stream, Message.loggedIn(user.Login));
                    userProgram(stream, ref user);
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
                _usersDatabase.updateLoginStatus(user);
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
                    if (!_usersDatabase.checkUserExist(userName))
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

                _usersDatabase.addUser(user.Login, user.Pass);
            }
        }
        #endregion

        #region USER_PROGRAM

        bool whiteSpace(string text)
        {
            if (text.Contains(" "))
                return true;
            else
                return false;
        }

        string getFileName(NetworkStream stream, int id, string mode)
        {
            WriteMessage(stream, Message.giveFileNameFILE);
            string fileName = ReadMessage(stream);

            if (whiteSpace(fileName) || fileName.Length == 0)
                fileName = "";


            if(mode == "add")
            {
                while (true)
                {
                    if (_filesDatabase.fileExists(fileName, id) || fileName.Length == 0 || whiteSpace(fileName)|| (fileName.Contains("\r\n") && fileName.Length == 2))
                    {
                        WriteMessage(stream, Message.fileIsExistsFILE);
                        WriteMessage(stream, Message.giveFileNameFILE);
                        fileName = ReadMessage(stream);
                    }
                    else if (fileName.Contains("\r\n") && fileName.Length == 2)
                        fileName = "";
                    else
                        break;
                    
                }
            }
            else if (mode == "del")
            {
                while (!_filesDatabase.fileExists(fileName, id))
                {
                    WriteMessage(stream, Message.fileDoesNotExistsFILE);
                    WriteMessage(stream, Message.giveFileNameFILE);
                    fileName = ReadMessage(stream);
                }
            }
            return fileName;
        }

        #region ADD_FILE
        void addFile(NetworkStream stream, int id)
        {
            string fileName = getFileName(stream, id, "add");

            WriteMessage(stream, Message.giveTextFILE);
            string text = ReadMessage(stream);
            _filesDatabase.addFile(fileName, text, id);

            if (_filesDatabase.fileExists(fileName, id))
            {
                WriteMessage(stream, Message.fileAddedFILE);
            }
            else
                WriteMessage(stream, Message.fileAddedErrorFILE);
        }
        #endregion

        #region LIST_FILE
        void listFile(NetworkStream stream, int id)
        {
            WriteMessage(stream, new ASCIIEncoding().GetBytes(_filesDatabase.getFileList(id)));
        }
        #endregion

        #region DELETE_FILE
        void deleteFile(NetworkStream stream, int id)
        {
            string fileName = getFileName(stream, id, "del");

            _filesDatabase.deleteFile(fileName, id);

            if (!_filesDatabase.fileExists(fileName, id))
                WriteMessage(stream, Message.deleteFILE);
            else
                WriteMessage(stream, Message.deleteErrorFILE);
        }
        #endregion

        #region UPDATE_FILE
        void updateFile(NetworkStream stream, int id)
        {
            string fileName = getFileName(stream, id, "del");
            WriteMessage(stream, Message.giveTextFILE);
            string text = ReadMessage(stream);

            if (_filesDatabase.updateFile(fileName, id, text))
                WriteMessage(stream, Message.updateFILE);
            else
                WriteMessage(stream, Message.updateErrorFILE);

        }
        #endregion

        #region OPEN_FILE

        void openFile(NetworkStream stream, int id)
        {
            string fileName = getFileName(stream, id, "del");
            WriteMessage(stream, new ASCIIEncoding().GetBytes(_filesDatabase.openFile(fileName, id)));
        }
        #endregion
        void userProgram(NetworkStream stream, ref Account user)
        {
            int id = (int)user.Id;
            bool choose = true;
            while(choose)
            {
                int x;
                WriteMessage(stream, Message.optionsPROGRAM);
                int.TryParse(ReadMessage(stream), out x);

                switch(x)
                {
                    case 1:
                        addFile(stream, id);
                        break;
                    case 2:
                        updateFile(stream, id);
                        break;
                    case 3:
                        deleteFile(stream, id);
                        break;
                    case 4:
                        openFile(stream, id);
                        break;
                    case 5:
                        listFile(stream, id);
                        break;
                    case 0:
                        choose = false;
                        break;
                    default:
                        WriteMessage(stream, Message.invalidOptionPROGRAM);
                        break;
                }
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
