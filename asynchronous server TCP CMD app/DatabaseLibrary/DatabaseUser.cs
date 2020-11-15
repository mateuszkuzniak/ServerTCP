using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class DatabaseUser : DatabaseAbstract
    {
        string _tableName;

        public string TableName { get => _tableName; set => _tableName = value; }

        public DatabaseUser(string databaseName, string tableName) : base(databaseName)
        {

            if (File.Exists("./database." + databaseName))
            {
                _myDatabaseConnection.Open();

                if(!checkForTableExist(tableName))
                {
                    _command.CommandText = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_name varchar(50) NOT NULL UNIQUE,
                        password varchar(255) NOT NULL,
                        isLogged BOOLEAN DEFAULT '0')";
                    _command.ExecuteNonQuery();

                    if (checkForTableExist(tableName))
                    {
                        Console.WriteLine($"{tableName} table has been created");
                    }
                    else
                        Console.WriteLine($"{tableName} table not created");
                }
                else
                {
                    openConnection();
                    _command.CommandText = $"UPDATE {tableName} SET isLogged = '0' WHERE isLogged = '1'";
                    _command.ExecuteNonQuery();
                }

                TableName = tableName;


            }
            else
            {
                openConnection();
                _command.CommandText = $"UPDATE {databaseName} SET isLogged = '0' WHERE isLogged = '1'";
                _command.ExecuteNonQuery();
            }
        }

        public bool checkUserExist(string userName)
        {
            openConnection();
            string userNameToLower = userName.ToLower();
            lock(keyLock)
            {
                _command.CommandText = $"SELECT id FROM {TableName} WHERE user_name = '{userNameToLower}'";
                if (_command.ExecuteScalar() != null)
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
            if (checkForTableExist(TableName)) 
            {
                try
                {
                    lock(keyLock)
                    {
                        _command.CommandText = $"INSERT INTO {TableName} (user_name, password)" +
                                             "VALUES('" + name + "','" + pass + "')";
                        _command.ExecuteNonQuery();
                    }

                    Console.WriteLine($"{TableName} added successfully");
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
                _command.CommandText = $"SELECT * FROM {TableName} WHERE user_name = '{userNameToLower}'";
                SQLiteDataReader reader = _command.ExecuteReader();

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
                    _command.CommandText = $"UPDATE {TableName} SET isLogged = '0' WHERE id='{account.Id}'";
                    account.IsLogged = false;
                }
                else
                {
                    _command.CommandText = $"UPDATE {TableName} SET isLogged = '1' WHERE id='{account.Id}'";
                    account.IsLogged = true;
                }
                _command.ExecuteNonQuery();
            }
        }
    }
}
