using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageLibrary
{
    public class Logger
    {
        private delegate void SafeCallDelegate(string text);
        public static TextBox Logs { get; set; }
        public static TextBox UsersList { get; set; }

        private string directoryLogsName = "Logs";
        private string serverLogsFileName = "serverLogs.txt";
        private string directoryLogsPath = "./Logs/";

        private void cw(string mess)
        {
            if (Logs.InvokeRequired)
            {
                var d = new SafeCallDelegate(cw);
                Logs.Invoke(d, new object[] { mess });
            }
            else
            {
                Logs.AppendText(mess);
                Logs.AppendText(Environment.NewLine);
            }
        }

        protected void getUserList(string mess)
        {
            if (Logs.InvokeRequired)
            {
                var d = new SafeCallDelegate(getUserList);
                UsersList.Invoke(d, new object[] { mess });
            }
            else
            {
                UsersList.Text = Time();
                UsersList.AppendText(Environment.NewLine);
                UsersList.AppendText(mess);
            }
        }

        private void saveLogs(string mess, string userName = null)
        {
            checkDirectory(directoryLogsName);
            var path = $"{directoryLogsPath}{((userName != null) ? $"{userName}.txt" : serverLogsFileName)}";
            mess = $"{Date()} {mess}";

            cw(mess);

            checkFileExist(path, mess);
            if (!path.Contains(serverLogsFileName))
                checkFileExist($"{directoryLogsPath}{serverLogsFileName}", mess);
        }

        private void checkFileExist(string path, string mess)
        {
            if (!File.Exists(path))
                File.WriteAllText(path, mess + Environment.NewLine);
            else
                File.AppendAllText(path, mess + Environment.NewLine);
        }

        private void checkDirectory(string name)
        {
            while (!Directory.Exists(name))
                Directory.CreateDirectory(name);
        }

        protected string getLogs(string userName)
        {
            var userPath = $"{directoryLogsPath}{userName}.txt";
            if (File.Exists(userPath))
            {
                return string.Join(Environment.NewLine, File.ReadAllLines(userPath));
            }
            else
                return null;
        }

        public static string invDbNameERROR = "Database name cannot be empty";
        string Date()
        {
            return DateTime.Now.ToString();
        }

        string Time()
        {
            return DateTime.Now.ToLongTimeString();
        }
        protected void error(string fun, string mes) { saveLogs($"ERROR({fun}): {mes}"); }

        #region DATABASE
        protected void createDatabaseLOG(string name) { saveLogs($"Database {name} has been created"); }
        protected void createTableLOG(string db, string name) { saveLogs($"Table database.{db}: {name} has been created"); }
        protected void existsTableLOG(string db, string name) { saveLogs($"Table database.{db}: {name} already exists"); }
        protected void newUserLOG(string name) { saveLogs($"User {name} added successfully"); }
        protected void addUserErrorLOG(string error) { saveLogs($"The specified username is taken. Error: {error}"); }

        #endregion

        #region SERVER
        protected void setPortServer(int port) { saveLogs($"Set Port: {port}"); }
        protected void setIpServer(string ip) { saveLogs($"Set IP: {ip}"); }
        protected void startServer() { saveLogs($"Server start listening"); }
        protected void connectClientServer(string ip) { saveLogs($"Client connected! IP:{ip}"); }
        protected void closeClientConnectionServer(int id = 0, string login = null) { saveLogs($"Connection with the cilent {((id != 0) ? $"({id}) { login}" : "")} has been terminated", login); }
        protected void exitClientServer(int id = 0, string login = null) { saveLogs($"Client {((id != 0) ? $"({id}) {login}" : "")} has logged out", login); }
        protected void loginAttempServer() { saveLogs("Login attemp"); }
        protected void loginUserServer(int? id, string login) { saveLogs($"User ({id}) {login} is logged in", login); }
        protected void registrationAttempServer() { saveLogs("Registration attemp"); }
        protected void userAddFileServer(int? id, string login, string fileName) { saveLogs($"User ({id}) {login} added a file: {fileName}", login); }
        protected void userDelFileServer(int? id, string login, string fileName) { saveLogs($"User ({id}) {login} deleted a file: {fileName}", login); }
        protected void userUpdateFileServer(int? id, string login, string fileName) { saveLogs($"User ({id}) {login} updated a file: {fileName}", login); }
        protected void userOpenFileServer(int? id, string login, string fileName) { saveLogs($"User ({id}) {login} opened a file: {fileName}", login); }
        protected void serverStop() { saveLogs("Work of the server has ended"); }
        #endregion


    }
}
