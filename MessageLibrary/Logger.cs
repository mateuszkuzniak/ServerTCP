using System;
using System.Collections.Generic;
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

        private void cw(string mess)
        {
            if (Logs.InvokeRequired)
            {
                var d = new SafeCallDelegate(cw);
                Logs.Invoke(d, new object[] { mess});
            }
            else
            {
                Logs.AppendText($"{Date()} {mess}");
                Logs.AppendText( Environment.NewLine);
            }
        }

        public static string invDbNameERROR = "Database name cannot be empty";
        string Date()
        {
            return DateTime.Now.ToString();
        }
        protected void error(string fun, string mes) { cw($"ERROR({fun}): {mes}"); }

        #region DATABASE
        protected void createDatabaseLOG(string name) { cw($"Database {name} has been created"); }
        protected void createTableLOG(string db, string name) { cw($"Table database.{db}: {name} has been created"); }
        protected void existsTableLOG(string db, string name) { cw($"Table database.{db}: {name} already exists"); }
        protected void newUserLOG(string name) { cw($"User {name} added successfully"); }
        protected void addUserErrorLOG(string error) { cw($"The specified username is taken. Error: {error}"); }

        #endregion

        #region SERVER
        protected void setPortServer(int port) { cw($"Set Port: {port}"); }
        protected void setIpServer(string ip) { cw($"Set IP: {ip}"); }
        protected void startServer() { cw($"Server start listening"); }
        protected void connectClientServer(string ip) { cw($"Client connected! IP:{ip}"); }
        protected void closeClientConnectionServer(int id = 0, string login = null) { cw($"Connection with the cilent {((id != 0) ? $"({id}) { login}" : "")} has been terminated"); }
        protected void exitClientServer(int id = 0, string login = null) { cw($"Client {((id != 0) ? $"({id}) {login}" : "")} has logged out"); }
        protected void loginAttempServer() { cw("Login attemp"); }
        protected void registrationAttempServer() { cw("Registration attemp"); }
        protected void userAddFileServer(int? id, string login) { cw($"User ({id}) {login} added a file"); }
        protected void userDelFileServer(int? id, string login) { cw($"User ({id}) {login} deleted a file"); }
        protected void userUpdateFileServer(int? id, string login) { cw($"User ({id}) {login} updated a file"); }
        protected void userOpenFileServer(int? id, string login) { cw($"User ({id}) {login} opened a file"); }
        #endregion

    }
}
