using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServerLibrary;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Server<LoginServerProtocol> server = new ServerTAP<LoginServerProtocol>(IPAddress.Parse("169.254.162.154"), 48569);
                server.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
