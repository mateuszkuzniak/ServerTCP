using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public abstract class DatabaseAbstract
    {
        protected SQLiteCommand _command;
        protected SQLiteConnection _myDatabaseConnection;
        protected readonly object keyLock = new object();


        /// <summary>
        /// Konstruktor database. Jeżeli baza danych nie istnieje to ją tworzy
        /// </summary

        protected DatabaseAbstract(string databaseName)
        {
            _myDatabaseConnection = new SQLiteConnection($"Data Source=database.{databaseName}");
            _command = new SQLiteCommand(_myDatabaseConnection);

            if(!File.Exists($"./database.{databaseName}"))
            {
                SQLiteConnection.CreateFile(databaseName);
                Console.WriteLine($"{databaseName} was created");
            }
        }

        /// <summary>
        /// Funkcja sprawdza czy jest otwarte połączenie z bazą danych. Jeżeli nie to otwiera połączenie
        /// </summary>
        /// <param name="databaseConnection">połączenie SQLite</param>
        protected void openConnection()
        {
            if (_myDatabaseConnection != null && _myDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                _myDatabaseConnection.Open();
            }
        }

        /// <summary>
        /// Funckja sprawdza czy jest zamknięte połączenie z bazą danych. Jeżeli nie to zamyka połączenie
        /// </summary>
        protected void cloceConnection()
        {
            if (_myDatabaseConnection != null && _myDatabaseConnection.State == System.Data.ConnectionState.Open)
            {
                _myDatabaseConnection.Close();
            }
        }

        /// <summary>
        /// Sprawdza czy tabela została utworzona
        /// </summary>
        /// <param name="tableName">nazwa tabeli do sprawdzenia</param>
        /// <param name="databaseConnection"> połączenie SQLite </param>
        /// <returns></returns>
        protected bool checkForTableExist(string tableName)
        {
            bool exists;
            openConnection();


                try
                {
                    _command.CommandText =
                    $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end";
                    exists = (int)_command.ExecuteScalar() == 1;
                }
                catch
                {
                    try
                    {
                        exists = true;
                        SQLiteCommand cmdOther = new SQLiteCommand($"SELECT 1 FROM {tableName} WHERE 1=0", _myDatabaseConnection);
                        cmdOther.ExecuteNonQuery();
                    }
                    catch
                    {
                        exists = false;
                    }
                }

            return exists;
        }
    }
}
