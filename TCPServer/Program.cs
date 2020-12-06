using System;
using System.Net;
using ServerLibrary;
using Exceptions;
using System.Net.Sockets;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Server<LoginServerProtocol> server = new ServerTAP<LoginServerProtocol>(IPAddress.Parse("192.168.42.74"), 8000);
                server.Start();
            }
            catch(CloseServerException)
            {
                Console.WriteLine("The server has shut down");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
