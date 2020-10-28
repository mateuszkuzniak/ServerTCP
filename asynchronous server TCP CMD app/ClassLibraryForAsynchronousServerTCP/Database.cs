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
        string databaseName = "database.users";
        SQLiteCommand command;

        /// <summary>
        /// Sprawdza czy jest otwarte połączenie z bazą danych. Jeżeli nie to otwiera połączenie
        /// </summary>
        /// <param name="databaseConnection">połączenie SQLite</param>
        public void openConnection()
        {
            if(myDatabaseConnection != null && myDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                myDatabaseConnection.Open();
            }
        }

        /// <summary>
        /// Sprawdza czy tabela została utworzona
        /// </summary>
        /// <param name="tableName">nazwa tabeli do sprawdzenia</param>
        /// <param name="databaseConnection"> połączenie SQLite </param>
        /// <returns></returns>
        private bool checkForTableExist(string tableName, SQLiteConnection databaseConnection) 
        {
            bool exists;
            openConnection();

            try
            {
                command = new SQLiteCommand (
                    "select case when exists((select * from information_schema.tables where table_name = '" + tableName + "')) then 1 else 0 end", databaseConnection);
                exists = (int)command.ExecuteScalar() == 1;
            }
            catch
            {
                try
                {
                    exists = true;
                    SQLiteCommand cmdOther = new SQLiteCommand("SELECT 1 FROM" + tableName + " WHERE 1=0", databaseConnection);
                    cmdOther.ExecuteNonQuery();
                }
                catch
                {
                    exists = false;
                }
            }

            return exists;
        }

        /// <summary>
        /// Konstruktor database. Jeżeli baza danych nie istnieje to ją tworzy
        /// </summary>
        public Database()
        {
            myDatabaseConnection = new SQLiteConnection("Data Source=" + databaseName);
            command = new SQLiteCommand(myDatabaseConnection);

            if(!File.Exists("./" + databaseName))
            { 
                SQLiteConnection.CreateFile(databaseName);
                Console.WriteLine("Database file created");

                myDatabaseConnection.Open();

                command.CommandText = @"CREATE TABLE users(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_name varchar(50) NOT NULL UNIQUE,
                        password varchar(255) NOT NULL,
                        isLogged BOOLEAN DEFAULT '0')";
                command.ExecuteNonQuery();

                if (checkForTableExist("users", myDatabaseConnection))
                {
                    Console.WriteLine("Users table has been created");
                }
                else
                    Console.WriteLine("Users table not created");
            }
        }


    }
}
