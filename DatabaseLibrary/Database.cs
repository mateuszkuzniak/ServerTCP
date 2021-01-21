using System;
using System.Data.SQLite;
using MessageLibrary;


namespace DatabaseLibrary
{
    public abstract class DatabaseAbstract : Logger
    {
        public enum DatabaseType
        {
            File,
            User
        }

        protected SQLiteCommand _command;
        protected SQLiteConnection _myDatabaseConnection;
        protected string _databaseName;
        protected readonly object keyLock = new object();
        protected string _tableUsers;
        protected string _tableFiles;


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
                    OpenConnection();
                    CloseConnection();
                    createDatabaseLOG(databaseName);
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
                //_myDatabaseConnection.Open();
                OpenConnection();

                if (tableName.Length > 0)
                {
                    if (!checkForTableExist(tableName))
                    {
                        try
                        {
                            _command.CommandText = command;
                            _command.ExecuteNonQuery();
                            createTableLOG(_databaseName, tableName);
                        }
                        catch (Exception e)
                        {
                            error("Database.CreateTable", e.Message);
                        }

                        if (checkForTableExist(tableName))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        existsTableLOG(_databaseName, tableName);
                        return false;
                    }
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
        
        private string AddSeparator(string data, string separator)
        {
            return data += separator;
        }

        public string GetListData(int id, DatabaseType type)
        {
            OpenConnection();
            string tableName;
            string data = "";

            if (type == DatabaseType.File)
                tableName = _tableFiles;
            else
                tableName = _tableUsers;


            if (checkForTableExist(tableName))
            {
                lock (keyLock)
                {
                    if (type == DatabaseType.File)
                        _command.CommandText = $"SELECT * FROM {tableName} WHERE userId = '{id}'";
                    else
                        _command.CommandText = $"SELECT * FROM {tableName} WHERE id = '{id}'";

                    SQLiteDataReader reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        if(type==DatabaseType.File)
                        {
                            data += reader.GetString(2);
                            data = AddSeparator(data, ";");
                        }
                        if(type == DatabaseType.User)
                        {
                            if (!reader.IsDBNull(4))
                                data += reader.GetString(4);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(5))
                                data += reader.GetString(5);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(6))
                                data += reader.GetString(6);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(7))
                                data += reader.GetInt32(7).ToString();
                            data = AddSeparator(data, ";");

                        }
                    }

                    reader.Close();
                }
            }

            if (data.Length > 0)
                return data;
            else
                return DbMessage.invFileListERROR;
        }
    }
}
