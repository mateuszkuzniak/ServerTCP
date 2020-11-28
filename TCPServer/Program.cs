using System;
using System.Net;
using ServerLibrary;
using Exceptions;

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
            catch(CloseServerException)
            {
                Console.WriteLine("The server has shut down");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
