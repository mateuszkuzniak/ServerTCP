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
        readonly object keyLock = new object();


        /// <summary>
        /// Funkcja sprawdza czy jest otwarte połączenie z bazą danych. Jeżeli nie to otwiera połączenie
        /// </summary>
        /// <param name="databaseConnection">połączenie SQLite</param>
        public void openConnection()
        {
            if (myDatabaseConnection != null && myDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                myDatabaseConnection.Open();
            }
        }

        /// <summary>
        /// Funckja sprawdza czy jest zamknięte połączenie z bazą danych. Jeżeli nie to zamyka połączenie
        /// </summary>
        public void cloceConnection()
        {
            if (myDatabaseConnection != null && myDatabaseConnection.State == System.Data.ConnectionState.Open)
            {
                myDatabaseConnection.Close();
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
                lock (keyLock)
                {
                    command.CommandText =
                    $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end";
                    exists = (int)command.ExecuteScalar() == 1;
                }

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
            lock (keyLock)
            {
                myDatabaseConnection = new SQLiteConnection("Data Source=" + databaseName);
                command = new SQLiteCommand(myDatabaseConnection);


                if (!File.Exists("./" + databaseName))
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

        public bool checkUserExist(string userName)
        {
            openConnection();
            string userNameToLower = userName.ToLower();
            lock (keyLock)
            {
                command.CommandText = $"SELECT id FROM users WHERE user_name = '{userNameToLower}'";
                if (command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Funkcja dodająca użytkowników do bazy danych 
        /// </summary>
        /// <param name="name">nazwa użytkownika</param>
        /// <param name="pass">hasło użytkownika</param>
        public void addUser(string name, string pass)
        {
            openConnection();
            if (checkForTableExist("users", myDatabaseConnection)) ;
            {
                try
                {
                    lock (keyLock)
                    {
                        command.CommandText = "INSERT INTO users(user_name, password)" +
                                                 "VALUES('" + name + "','" + pass + "')";
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("User added successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine("The specified username is taken. Error: " + e.GetHashCode());
                }
            }

        }

        /// <summary>
        /// Funkcja wyszukująca użytkownika w bazie danych z użyciem nazwy użytkownika
        /// </summary>
        /// <param name="user_name">nazwa użytkownika</param>
        /// <returns></returns>
        public Account getUserWithDatabase(string userName)
        {
            openConnection();
            string userNameToLower = userName.ToLower();
            Account user = new Account();
            lock (keyLock)
            {
                command.CommandText = $"SELECT * FROM users WHERE user_name = '{userNameToLower}'";
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.Id = reader.GetInt16(0);
                    user.Login = reader.GetString(1);
                    user.Pass = reader.GetString(2);
                    user.IsLogged = reader.GetBoolean(3);
                }

                reader.Close();
            }
            return user;
        }


        /// <summary>
        /// Funkcja zmienia status użytkownika: zalogowany/wylogowany
        /// </summary>
        /// <param name="account">Konto</param>
        public void updateLoginStatus(Account account)
        {
            openConnection();

            lock(keyLock)
            {
                if (account.IsLogged)
                {
                    command.CommandText = $"UPDATE users SET isLogged = '0' WHERE id='{account.Id}'";
                    account.IsLogged = false;
                }
                else
                {
                    command.CommandText = $"UPDATE users SET isLogged = '1' WHERE id='{account.Id}'";
                    account.IsLogged = true;
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
