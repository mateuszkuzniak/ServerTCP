using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLibrary
{
    public static class ServerMessage
    {
        #region LOGIN
        public static string invUser = "INV_USER";
        public static string currentlyLogged = "USER IS CURRENTLY LOGGED IN ";
        public static string invPwd = "INV_PWD";
        public static string logged = "LOGGED";
        public static string alreadyLogged = "YOU ARE ALREADY LOGGED IN";
        public static string userExists = "USER_EXISTS";
        public static string regOk = "REG_OK";
        public static string unk = "UNK";
        #endregion

        #region FILE
        public static string fileAdd = "FILE_ADD";
        public static string fileExists = "FILE_EXISTS";
        public static string mustBelogged = "MUST_BE_LOGGED";
        public static string invFileName = "INV_FILE_NAME";
        public static string fileDeleted = "FILE_DELETED";
        public static string fileDeletedError = "FILE_DELETED_ERROR";
        public static string fileUpdate = "FILE_UPDATE";


        #endregion

        public static string invComm = "INV_COMM";

        #region SERVER
        public static string incorrectPort = "Incorrect port value, set the port to 8000";
        #endregion

        public static string CloseConnection(string userName)
        {
            return $"Connection with the cilent {userName} has been terminated";
        }

    }
}
