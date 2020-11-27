using System;

using System.Data.SQLite;
using System.IO;

using ExceptionLibrary;

namespace DatabaseLibrary
{
    public class Database
    {
        protected SQLiteCommand _command;
        protected SQLiteConnection _myDatabaseConnection;
        protected string _databaseName;
        protected readonly object keyLock = new object();


        protected Database(string databaseName)
        {
            if (databaseName != null)
            {
                _myDatabaseConnection = new SQLiteConnection($"Data Source=database.{databaseName}");
                _command = new SQLiteCommand(_myDatabaseConnection);
                _databaseName = databaseName;

                if (!checkForDatabaseExists())
                {
                    SQLiteConnection.CreateFile(databaseName);
                    Console.WriteLine($"{databaseName} was created");
                }
            }
            else
                throw new Exception(Error.emptyName);
        }

        #region CONNECTION
        protected void OpenConnection()
        {
            if (_myDatabaseConnection != null && _myDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                _myDatabaseConnection.Open();
            }
        }

        protected void CloseConnection()
        {
            if (_myDatabaseConnection != null && _myDatabaseConnection.State == System.Data.ConnectionState.Open)
            {
                _myDatabaseConnection.Close();
            }
        }
        #endregion


        protected bool checkForDatabaseExists()
        {
            if (System.IO.File.Exists($"./database.{_databaseName}"))
                return true;
            else
                return false;
        }


        protected bool CreateTable(string tableName, string command)
        {
            if (checkForDatabaseExists())
            {
                _myDatabaseConnection.Open();

                if (!checkForTableExist(tableName))
                {
                    try
                    {
                        _command.CommandText = command;
                        _command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }

                    if (checkForTableExist(tableName))
                    {
                        Console.WriteLine($"{tableName} table has been created");
                        return true;
                    }
                }
                else
                    return false;
            }

            return false;
        }
        protected bool checkForTableExist(string tableName)
        {
            bool exists;
            OpenConnection();


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
