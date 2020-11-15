using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerLibrary
{

    public static class Message
    {

        #region MENU
        /// <summary>
        /// Hello!\n\r 1. Log in\n\r2. Register:\n\r
        /// </summary>
        public static readonly byte[] helloMessageMENU = new ASCIIEncoding().GetBytes("Hello!\n\r1. Log in\n\r2. Register:\n\r");
        /// <summary>
        /// ERROR: This option does not exist
        /// </summary>
        public static readonly byte[] menuErrorMENU = new ASCIIEncoding().GetBytes("ERROR: This option does not exist\n\r");
        /// <summary>
        /// ERROR: Just enter the number
        /// </summary>
        public static readonly byte[] invalidCharErrorMENU = new ASCIIEncoding().GetBytes("ERROR: Just enter the number\n\r");
        #endregion

        #region LOGIN
        /// <summary>
        /// Give login
        /// </summary>
        public static readonly byte[] giveLoginLOGIN = new ASCIIEncoding().GetBytes("Give login\n\r");
        
        /// <summary>
        /// ERROR: User does not exist
        /// </summary>
        public static readonly byte[] userDoesNotExistsErrorLOGIN = new ASCIIEncoding().GetBytes("ERROR: User does not exist\n\r");
        
        /// <summary>
        /// Give password
        /// </summary>
        public static readonly byte[] givePasswordLOGIN = new ASCIIEncoding().GetBytes("Give password\n\r");
        
        /// <summary>
        /// ERROR: Bad password
        /// </summary>
        public static readonly byte[] badPasswordErrorLOGIN = new ASCIIEncoding().GetBytes("ERROR: Bad password\n\r");
        
        /// <summary>
        /// Hello {userName}! Logged in correctly
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns></returns>
        public static byte[] loggedIn(string userName)
        {
            byte[] text = new ASCIIEncoding().GetBytes($"Hello {userName}! Logged in correctly \n\r");
            return text;
        }
        
        /// <summary>
        /// ERROR: User is logged
        /// </summary>
        public static readonly byte[] userIsLoggedError = new ASCIIEncoding().GetBytes("ERROR: User is logged\n\r");
        
        /// <summary>
        /// Press The key to log out
        /// </summary>
        public static readonly byte[] logOut = new ASCIIEncoding().GetBytes("Press The key to log out\n\r");

        /// <summary>
        /// The user has been logged out
        /// </summary>
        public static readonly byte[] userLogOut  = new ASCIIEncoding().GetBytes("\nThe user has been logged out\n\r");

        #endregion

        #region REGISTRY
        /// <summary>
        /// Give user name
        /// </summary
        public static readonly byte[] giveUserNameREGISTRY = new ASCIIEncoding().GetBytes("Give user name\n\r");

        /// <summary>
        /// Username can onlu contain letters and numbers
        /// </summary>
        public static readonly byte[] invalidUserNameREGISTRY = new ASCIIEncoding().GetBytes("Username can only contain letters and numbers. Word length must be longer than 2\n\r");

        /// <summary>
        /// The specified username is taken
        /// </summary>
        public static readonly byte[] busyUserNameREGISTRY = new ASCIIEncoding().GetBytes("The specified username is taken\n\r");

        /// <summary>
        /// Enter the password
        /// </summary>
        public static readonly byte[]   enterPasswordREGISTRY = new ASCIIEncoding().GetBytes("Enter the password\n\r");

        /// <summary>
        /// Password musy contains: -> 8 character, -> one upper and lower case letter, -> one number and a special character"
        /// </summary>
        public static readonly byte[]  invalidPasswordREGISTRY = new ASCIIEncoding().GetBytes("Password must contains:\n\r-> 8 character," +
                                                                                                "\n\r-> one upper and lower case letter," +
                                                                                                "\n\r-> one number and a special character" +
                                                                                                "\n\r-> username can't be equal to \"break\"");

        /// <summary>
        /// User added successfully
        /// </summary>
        public static readonly byte[] userAddedREGISTRY = new ASCIIEncoding().GetBytes("User added successfully\n\r");

        #endregion

        #region CONNECTION

        /// <summary>
        /// Connection with the user has been lost
        /// </summary>
        public static readonly string lostConnectionCONNECTION = "Connection with the user has been lost ";
        
        /// <summary>
        /// Connection lost!\nCleaning...
        /// </summary>
        public static readonly string connectionCloseCONNECTION = "Connection lost!\nCleaning...";

        #endregion

        #region FILE
        public static readonly byte[] giveFileNameFILE = new ASCIIEncoding().GetBytes("Give file name\n\r");
        public static readonly byte[] giveTextFILE= new ASCIIEncoding().GetBytes("Give text\n\r");
        public static readonly byte[] fileIsExistsFILE= new ASCIIEncoding().GetBytes("File exist, please enter a different name\n\r");
        public static readonly byte[] fileDoesNotExistsFILE= new ASCIIEncoding().GetBytes("File does not exist, please enter a correct name\n\r");
        public static readonly byte[] fileAddedFILE= new ASCIIEncoding().GetBytes("Successfully added file!\n\r");
        public static readonly byte[] fileAddedErrorFILE= new ASCIIEncoding().GetBytes("Filed to add file\n\r");
        public static readonly byte[] deleteFILE= new ASCIIEncoding().GetBytes("The file has been deleted\n\r");
        public static readonly byte[] deleteErrorFILE= new ASCIIEncoding().GetBytes("The file was not deleted\n\r");
        public static readonly byte[] updateFILE= new ASCIIEncoding().GetBytes("File update successfully\n\r");
        public static readonly byte[] updateErrorFILE= new ASCIIEncoding().GetBytes("The file has not been update\n\r");
        #endregion

        #region USER_PROGRAM
        public static readonly byte[] optionsPROGRAM = new ASCIIEncoding().GetBytes("Options available\n\r1.Add file 2.Update file 3.Delete file 4.Open file 5.List file 0.EXIT\n\r");
        public static readonly byte[] invalidOptionPROGRAM = new ASCIIEncoding().GetBytes("This option is not available!\n\r");
        #endregion
    }
}
