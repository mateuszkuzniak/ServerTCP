using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryForAsynchronousServerTCP;

namespace asynchronous_server_TCP_CMD_app
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database databaseObject = new Database();
            //databaseObject.addUser("Paweł", "jakiestamhaslo123");
            //databaseObject.updateLoginStatus(databaseObject.getUserWithDatabase("Mateusz").First());
            TcpServerAPM serverObj = new TcpServerAPM(IPAddress.Parse("127.0.0.1"), 8000);
            serverObj.Start();

            Console.ReadLine();
        }
    }
}
