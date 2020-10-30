﻿using System;
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
                command.CommandText = 
                    $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end";
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

        public bool checkUserExist(string user_name)
        {
            openConnection();
            user_name.ToLower();
            command.CommandText = $"SELECT id FROM users WHERE user_name = '{user_name}'";
            if (command.ExecuteScalar() != null)
                return true;
            else
                return false;
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
                    command.CommandText = "INSERT INTO users(user_name, password)" +
                                           "VALUES('" + name + "','" + pass + "')";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Dodano użytownika pomyślnie");
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
        public List<Account> getUserWithDatabase(string user_name)
        {
            openConnection();

            command.CommandText = "SELECT * FROM users WHERE user_name = '" + user_name + "'";
            SQLiteDataReader reader = command.ExecuteReader();
            List<Account> user = new List<Account>();
            while (reader.Read())
            {
                user.Add(new Account
                {
                    id = reader.GetInt16(0),
                    login = reader.GetString(1),
                    pass = reader.GetString(2),
                    isLogged = reader.GetBoolean(3)
                });
            }
            reader.Close();
            return user;
        }


        /// <summary>
        /// Funkcja zmienia status użytkownika: zalogowany/wylogowany
        /// </summary>
        /// <param name="account">Konto</param>
        public void updateLoginStatus(Account account)
        {
            openConnection();

            if (account.isLogged)
                command.CommandText = $"UPDATE users SET isLogged = '0' WHERE id='{account.id}'";
            else
                command.CommandText = $"UPDATE users SET isLogged = '1' WHERE id='{account.id}'";
            command.ExecuteNonQuery();
        }
    }
} 
