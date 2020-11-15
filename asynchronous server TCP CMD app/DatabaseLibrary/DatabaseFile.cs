using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class DatabaseFile : DatabaseAbstract
    {
        string _tableName;
        public string TableName { get => _tableName; set => _tableName = value; }
        public DatabaseFile(string databaseName, string tableName) : base(databaseName)
        {

            if (File.Exists("./database." + databaseName))
            {
                _myDatabaseConnection.Open();

                if (!checkForTableExist(tableName))
                {
                    _command.CommandText = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        userId INTEGER NOT NULL,
                        fileName varchar(255) NOT NULL,
                        textFile TEXT)";
                    _command.ExecuteNonQuery();

                    if (checkForTableExist(tableName))
                    {
                        Console.WriteLine($"{tableName} table has been created");
                    }
                    else
                        Console.WriteLine($"{tableName} table not created");
                }

            }

            TableName = tableName;
        }

        public bool fileExists(string fileName, int userId)
        {
            openConnection();
            string nameToLower = fileName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT id FROM {TableName} WHERE fileName = '{nameToLower}' AND userId = {userId}";
                if (_command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }

        }

        public bool addFile(string fileName, string text, int id)
        {
            string nameToLower = fileName.ToLower();
            openConnection();
            if (checkForTableExist(TableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"INSERT INTO {TableName} (userId, fileName, textFile)" +
                                             $"VALUES('{id}','{nameToLower}','{text}')";
                        _command.ExecuteNonQuery();
                    }

                    if (fileExists(fileName, id))
                        return true;
                    else
                        return false;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                    return false;
                }
            }
            else
                return false;
        }

        public void deleteFile(string fileName, int id)
        {
            fileName = fileName.ToLower();
            openConnection();
            if (checkForTableExist(TableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"DELETE FROM {TableName} WHERE userId = '{id}' AND fileName = '{fileName}'";
                        _command.ExecuteNonQuery();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
        }

        public bool updateFile(string fileName, int id, string newText)
        {
            fileName = fileName.ToLower();
            openConnection();
            if (checkForTableExist(TableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"UPDATE {TableName} SET textFile = '{newText}' WHERE userId = '{id}' AND fileName = '{fileName}'";
                        _command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
            return false;
        }

        public string getFileList(int id)
        {
            openConnection();
            string fileList = "";

            if (checkForTableExist(TableName))
            {
                lock (keyLock)
                {
                    _command.CommandText = $"SELECT * FROM {TableName} WHERE userId = '{id}'";
                    SQLiteDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        fileList += reader.GetString(2);
                        fileList += "\n\r";
                    }


                    reader.Close();
                }
            }

            return fileList;
        }

        public string openFile(string fileName, int id)
        {
            fileName = fileName.ToLower();
            openConnection();
            string fileList = "";

            if (checkForTableExist(TableName))
            {
                lock (keyLock)
                {
                    _command.CommandText = $"SELECT * FROM {TableName} WHERE userId = '{id}' AND fileName ='{fileName}'";
                    SQLiteDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        fileList += reader.GetString(2);
                        fileList += ": ";
                        fileList += reader.GetString(3);
                        fileList += "\n\r";
                    }


                    reader.Close();
                }
            }

            return fileList;
        }
    }
}
