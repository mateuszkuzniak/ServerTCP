using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibraryForAsynchronousServerTCP
{

    public static class Message
    {

        #region MENU
        /// <summary>
        /// Hello!\n\r 1. Log in\n\r2. Register:\n\r
        /// </summary>
        public static readonly byte[] helloMessage = new ASCIIEncoding().GetBytes("Hello!\n\r1. Log in\n\r2. Register:\n\r");
        /// <summary>
        /// ERROR: This option does not exist
        /// </summary>
        public static readonly byte[] menuError = new ASCIIEncoding().GetBytes("ERROR: This option does not exist\n\r");
        /// <summary>
        /// ERROR: Just enter the number
        /// </summary>
        public static readonly byte[] invalidCharError = new ASCIIEncoding().GetBytes("ERROR: Just enter the number\n\r");
        #endregion

        #region LOGIN
        /// <summary>
        /// Give login
        /// </summary>
        public static readonly byte[] giveLogin = new ASCIIEncoding().GetBytes("Give login\n\r");
        /// <summary>
        /// ERROR: User does not exist
        /// </summary>
        public static readonly byte[] userDoesNotExistsError = new ASCIIEncoding().GetBytes("ERROR: User does not exist\n\r");
        /// <summary>
        /// Give password
        /// </summary>
        public static readonly byte[] givePassword = new ASCIIEncoding().GetBytes("Give password\n\r");
        /// <summary>
        /// ERROR: Bad password
        /// </summary>
        public static readonly byte[] badPasswordError = new ASCIIEncoding().GetBytes("ERROR: Bad password\n\r");

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

        #endregion

        #region CONNECTION
        public static readonly string lostConnection = "Connection with the user has been lost ";
        #endregion





    }
}
