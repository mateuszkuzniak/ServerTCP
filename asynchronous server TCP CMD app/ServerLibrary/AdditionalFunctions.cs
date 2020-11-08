using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public static class AdditionalFunctions
    {
        /// <summary>
        /// Funkcja testująca bezpieczeństwo hasła
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool securePassword(string pass)
        {
            int bigLetters = 0, smallLeters = 0, numbers = 0, specialCharacter = 0;
            foreach (char x in pass)
            {
                if (((x >= 33) && (x <= 47)) || ((x >= 58) && (x <= 64)) || ((x >= 91) && (x <= 96)) || ((x >= 123) && (x <= 126)))
                    specialCharacter++;
                else if ((x >= 48) && x <= 57)
                    numbers++;
                else if ((x >= 65) && (x <= 90))
                    bigLetters++;
                else if ((x >= 97 && x <= 122))
                    smallLeters++;
            }
            if ((pass.Length > 7) && (bigLetters > 0) && (smallLeters > 0) && (numbers > 0) && (specialCharacter > 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Funkcja sprawdzająca poprawność nazwy użytkownika
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool validUserName(string userName)
        {
            int check = 0;
            if(userName != "break" || userName.Length>2)
            {
                foreach (char x in userName)
                {
                    if ((x >= 32 && x <= 47) || (x >= 58 && x <= 64) || (x >= 91 && x <= 96) || (x >= 123 && x <= 126))
                        check++;
                }
            }

            if ((check > 0) || (userName == "break") || (userName.Length<=2))
                return false;
            else
                return true;
        }
    }
}
