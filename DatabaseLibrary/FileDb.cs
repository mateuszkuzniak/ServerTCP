using System;
using System.Data.SQLite;
using MessageLibrary;

namespace DatabaseLibrary
{
    public class FileDb : Database
    {
        string _tableName;

        public FileDb(string databaseName, string tableName) : base(databaseName)
        {
            string command = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        userId INTEGER NOT NULL,
                        fileName varchar(255) NOT NULL,
                        textFile TEXT)";

            CreateTable(tableName, command);
            _tableName = tableName;

        }

        public bool FileExists(string fileName, int userId)
        {
            OpenConnection();
            string nameToLower = fileName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT id FROM {_tableName} WHERE fileName = '{nameToLower}' AND userId = {userId}";
                if (_command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }
        }

        public bool AddFile(string fileName, string text, int id)
        {
            string nameToLower = fileName.ToLower();
            OpenConnection();
            if (checkForTableExist(_tableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"INSERT INTO {_tableName} (userId, fileName, textFile)" +
                                             $"VALUES('{id}','{nameToLower}','{text}')";
                        _command.ExecuteNonQuery();
                    }

                    if (FileExists(fileName, id))
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

        public void DeleteFile(string fileName, int id)
        {
            fileName = fileName.ToLower();
            OpenConnection();
            if (checkForTableExist(_tableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"DELETE FROM {_tableName} WHERE userId = '{id}' AND fileName = '{fileName}'";
                        _command.ExecuteNonQuery();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
        }

        public bool UpdateFile(string fileName, int id, string newText)
        {
            fileName = fileName.ToLower();
            OpenConnection();
            if (checkForTableExist(_tableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"UPDATE {_tableName} SET textFile = '{newText}' WHERE userId = '{id}' AND fileName = '{fileName}'";
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

        public string GetFileList(int id)
        {
            OpenConnection();
            string fileList = "";

            if (checkForTableExist(_tableName))
            {
                lock (keyLock)
                {
                    _command.CommandText = $"SELECT * FROM {_tableName} WHERE userId = '{id}'";
                    SQLiteDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        fileList += reader.GetString(2);
                        fileList += ";";
                    }


                    reader.Close();
                }
            }

            if (fileList.Length > 0)
                return fileList;
            else
                return DbMessage.invFileListERROR;
        }
        public string openFile(string fileName, int id)
        {
            fileName = fileName.ToLower();
            OpenConnection();
            string fileList = "";

            if (checkForTableExist(_tableName))
            {
                lock (keyLock)
                {
                    _command.CommandText = $"SELECT * FROM {_tableName} WHERE userId = '{id}' AND fileName ='{fileName}'";
                    SQLiteDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
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
