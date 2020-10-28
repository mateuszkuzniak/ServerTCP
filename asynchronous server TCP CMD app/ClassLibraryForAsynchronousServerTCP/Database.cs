using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public class Database
    {
        public SQLiteConnection myDatabaseConnection;
        string databaseName = "database.userDatabase";

        public Database()
        {
            myDatabaseConnection = new SQLiteConnection("Data Source=" + databaseName);

            if(!File.Exists("./" + databaseName))
            { 
                SQLiteConnection.CreateFile(databaseName);
                Console.WriteLine("Database file created");
            }
        }


    }
}
