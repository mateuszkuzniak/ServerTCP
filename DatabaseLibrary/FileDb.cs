using System;
using System.Data.SQLite;
using MessageLibrary;

namespace DatabaseLibrary
{
    public class FileDb : DatabaseAbstract
    {
        public FileDb(string databaseName, string tableName) : base(databaseName)
        {
            string command = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        userId INTEGER NOT NULL,
                        fileName varchar(255) NOT NULL,
                        textFile TEXT)";

            CreateTable(tableName, command);
            _tableFiles = tableName;

        }

        public bool FileExists(string fileName, int userId)
        {
            OpenConnection();
            string nameToLower = fileName.ToLower();

            lock (_command)
            {
                _command.CommandText = $"SELECT id FROM {_tableFiles} WHERE fileName = '{nameToLower}' AND userId = {userId}";
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
            if (checkForTableExist(_tableFiles))
            {
                try
                {
                    lock (_command)
                    {
                        _command.CommandText = $"INSERT INTO {_tableFiles} (userId, fileName, textFile)" +
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
                    error("FileDb.AddFile", e.Message);
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
            if (checkForTableExist(_tableFiles))
            {
                try
                {
                    lock (_command)
                    {
                        _command.CommandText = $"DELETE FROM {_tableFiles} WHERE userId = '{id}' AND fileName = '{fileName}'";
                        _command.ExecuteNonQuery();
                    }

                }
                catch (Exception e)
                {
                    error("FileDb.AddFile", e.Message);
                }
            }
        }

        public bool UpdateFile(string fileName, int id, string newText)
        {
            fileName = fileName.ToLower();
            OpenConnection();
            if (checkForTableExist(_tableFiles))
            {
                try
                {
                    lock (_command)
                    {
                        _command.CommandText = $"UPDATE {_tableFiles} SET textFile = '{newText}' WHERE userId = '{id}' AND fileName = '{fileName}'";
                        _command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    error("FileDb.AddFile", e.Message);
                }
            }
            return false;
        }

        public string openFile(string fileName, int id)
        {
            fileName = fileName.ToLower();
            OpenConnection();
            string fileList = "";

            if (checkForTableExist(_tableFiles))
            {
                lock (_command)
                {
                    _command.CommandText = $"SELECT * FROM {_tableFiles} WHERE userId = '{id}' AND fileName ='{fileName}'";
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
