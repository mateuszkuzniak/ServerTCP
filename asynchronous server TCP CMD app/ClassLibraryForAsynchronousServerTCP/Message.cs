using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibraryForAsynchronousServerTCP
{

    public static class Message { 
    
        public static readonly byte[] helloMessage = new ASCIIEncoding().GetBytes("Witaj! Zaloguj się do serwera. Podaj login:\n\r");
    }
}
