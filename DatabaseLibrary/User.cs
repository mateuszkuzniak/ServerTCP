using System;
using System.Data.SQLite;
using MessageLibrary;

namespace DatabaseLibrary
{
    public class User : DatabaseAbstract
    {
        string _tableName;
        public string TableName {get => _tableName;}
        public User(string databaseName, string tableName) : base(databaseName)
        {
            string command = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_name varchar(50) NOT NULL UNIQUE,
                        password varchar(255) NOT NULL,
                        isLogged BOOLEAN DEFAULT '0')";

            CreateTable(tableName, command);

            if (checkForTableExist(tableName))
            {
                MakeLogOut(tableName);
            }
            else
            {
                CreateTable(tableName, command);
            }

            _tableName = tableName;
        }

        public void MakeLogOut(string tableName)
        {
            OpenConnection();
            _command.CommandText = $"UPDATE {tableName} SET isLogged = '0' WHERE isLogged = '1'";
            _command.ExecuteNonQuery();
        }
        public bool CheckUserExist(string userName)
        {
            OpenConnection();
            string userNameToLower = userName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT id FROM {_tableName} WHERE user_name = '{userNameToLower}'";
                if (_command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }
        }

        public bool UserIsLogged(string userName)
        {
            OpenConnection();
            string userNameToLower = userName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT id FROM {_tableName} WHERE user_name = '{userNameToLower}' AND isLogged = '1'";
                if (_command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }
        }

        public bool CheckPassword(string userName, string pass)
        {
            OpenConnection();
            string userNameToLower = userName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT id FROM {_tableName} WHERE user_name = '{userNameToLower}' AND password = '{pass}'";
                if (_command.ExecuteScalar() != null)
                    return true;
                else
                    return false;
            }
        }

        public bool ChangePassword(string userName, string oldPass, string newPass)
        {
            OpenConnection();
            userName = userName.ToLower();

            lock (keyLock)
            {
                _command.CommandText = $"UPDATE {_tableName} SET password = '{newPass}' WHERE user_name = '{userName}' AND password = '{oldPass}'";
                _command.ExecuteScalar();
                if(CheckPassword(userName, newPass))
                    return true;
                else
                    return false;
            }
        }

        public void AddUser(string name, string pass)
        {
            name = name.ToLower().Trim(new char[] { '\r', '\n', '\0' });
            OpenConnection();
            if (checkForTableExist(_tableName))
            {
                try
                {
                    lock (keyLock)
                    {
                        _command.CommandText = $"INSERT INTO {_tableName} (user_name, password)" +
                                             $"VALUES('{name}','{pass}')";
                        _command.ExecuteNonQuery();
                    }

                    newUserLOG(name);
                }
                catch (Exception e)
                {
                    addUserErrorLOG(e.Message);
                }
            }
        }

        public Account GetUserWithDatabase(string userName)
        {
            OpenConnection();
            string userNameToLower = userName.ToLower();
            Account user = new Account();

            lock (keyLock)
            {
                _command.CommandText = $"SELECT * FROM {_tableName} WHERE user_name = '{userNameToLower}'";
                SQLiteDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    user.Id = reader.GetInt16(0);
                    user.Login = reader.GetString(1);
                    user.IsLogged = reader.GetBoolean(3);
                }


                reader.Close();
            }

            return user;
        }

        public string GetAllLogedUser()
        {
            OpenConnection();
            string users = "";


            lock (keyLock)
            {
                _command.CommandText = $"SELECT * FROM {_tableName} WHERE isLogged = '1'";
                SQLiteDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    users += $"id: ({reader.GetInt16(0)}) ";
                    users += $"login: {reader.GetString(1)}";
                    users += Environment.NewLine;
                }


                reader.Close();
            }

            if (users.Length > 0)
                return users;
            else
                return DbMessage.userListEmpty;
        }

        public void UpdateLoginStatus(Account account)
        {
            OpenConnection();
            lock (keyLock)
            {
                if (account.IsLogged)
                {
                    _command.CommandText = $"UPDATE {_tableName} SET isLogged = '0' WHERE id='{account.Id}'";
                    account.IsLogged = false;
                }
                else
                {
                    _command.CommandText = $"UPDATE {_tableName} SET isLogged = '1' WHERE id='{account.Id}'";
                    account.IsLogged = true;
                }
                _command.ExecuteNonQuery();
            }
        }
    }
}
