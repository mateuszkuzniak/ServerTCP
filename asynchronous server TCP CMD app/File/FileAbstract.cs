using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    class FileAbstract : FileInterface
    {
        string name;
        string content;
        string type;
        
        public static string open()
        {
            throw new NotImplementedException();
        }
        
        public static string close()
        {
            throw new NotImplementedException();
        }

        public static void created()
        {
            throw new NotImplementedException();
        }

        public static void edit()
        {
            throw new NotImplementedException();
        }


    }
}
