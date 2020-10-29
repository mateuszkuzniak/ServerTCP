using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public static class AdditionalFunctions
    {
        public static bool securePassword(string pass)
        {
            int bigLetters = 0, smallLeters = 0, numbers = 0, specialCharacter = 0;
            foreach(char x in pass)
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
    }
}
