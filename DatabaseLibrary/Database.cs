using System;
using System.Data.SQLite;
using System.IO;
using MessageLibrary;

namespace DatabaseLibrary
{
    public abstract class DatabaseAbstract
    {
        protected SQLiteCommand _command;
        protected SQLiteConnection _myDatabaseConnection;
        protected string _databaseName;
        protected readonly object keyLock = new object();


        protected DatabaseAbstract(string databaseName)
        {
            if (databaseName != null)
            {
                _myDatabaseConnection = new SQLiteConnection($"Data Source=database.{databaseName}");
                _command = new SQLiteCommand(_myDatabaseConnection);
                _databaseName = databaseName;

                if (!checkForDatabaseExists())
                {
                    SQLiteConnection.CreateFile(databaseName);
                    Console.WriteLine(DbMessage.CreateDatabase(databaseName));
                    OpenConnection();
                    CloseConnection();
                }
            }
            else
                throw new Exception(DbMessage.invDbNameERROR);
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

                if (tableName.Length > 0)
                {
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
                            Console.WriteLine();
                            return true;
                        }
                    }
                    else
                        return false;
                }
                else throw new Exception(DbMessage.invTableNameERROR);
            }

            return false;
        }
        protected bool checkForTableExist(string tableName)
        {
            bool exists;
            OpenConnection();


            if (tableName.Length > 0)
            {
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
            }
            else
                throw new Exception(DbMessage.invTableNameERROR);

            return exists;
        }
    }
}
