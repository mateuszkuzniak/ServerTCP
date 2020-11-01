using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibraryForAsynchronousServerTCP
{

    public static class Message { 
    
        public static readonly byte[] helloMessage = new ASCIIEncoding().GetBytes("Hello!\n\r 1. Log in\n\r2. Register:\n\r");
        public static readonly byte[] menuError= new ASCIIEncoding().GetBytes("ERROR: This option does not exist\n\r");
        public static readonly byte[] invalidChar= new ASCIIEncoding().GetBytes("ERROR: Just enter the number\n\r");

    }
}
