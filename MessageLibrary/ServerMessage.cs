
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
        public static string userDoesNotExists = "USER_DOES_NOT_EXISTS";
        public static string regOk = "REG_OK";
        public static string unk = "UNK";
        public static string changePwd = "CHANGE_PWD";
        public static string changePwdError = "CHANGE_PWD_ERROR";
        #endregion

        #region USER
        public static string invMail = "INV_MAIL";
        public static string invFirstName = "INV_FIRST_NAME";
        public static string invSecondName = "INV_SECOND_NAME";
        public static string invPhoneNumber = "INV_PHONE_NUMBER";
        public static string userValidData = "USER_VALID_DATA";
        public static string userInvData = "USER_INV_DATA";
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


    }
}
