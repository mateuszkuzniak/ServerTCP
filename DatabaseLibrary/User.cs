using System;
using System.Data.SQLite;
using MessageLibrary;

namespace DatabaseLibrary
{
    public class User : DatabaseAbstract
    {
        public string TableName { get => _tableUsers; }
        public User(string databaseName, string tableName) : base(databaseName)
        {
            string command = $@"CREATE TABLE {tableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user_name varchar(50) NOT NULL UNIQUE,
                        password varchar(255) NOT NULL,
                        isLogged BOOLEAN DEFAULT '0',
                        email varchar(255),
                        firstName varchar(255),
                        secondName varchar(255), 
                        phone int(9) CHECK(phone BETWEEN 100000000 AND 999999999))";

            CreateTable(tableName, command);

            if (checkForTableExist(tableName))
            {
                MakeLogOut(tableName);
            }
            else
            {
                CreateTable(tableName, command);
            }

            _tableUsers = tableName;
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

            lock (_command)
            {
                _command.CommandText = $"SELECT id FROM {_tableUsers} WHERE user_name = '{userNameToLower}'";
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

            lock (_command)
            {
                _command.CommandText = $"SELECT id FROM {_tableUsers} WHERE user_name = '{userNameToLower}' AND isLogged = '1'";
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

            lock (_command)
            {
                _command.CommandText = $"SELECT id FROM {_tableUsers} WHERE user_name = '{userNameToLower}' AND password = '{pass}'";
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

            lock (_command)
            {
                _command.CommandText = $"UPDATE {_tableUsers} SET password = '{newPass}' WHERE user_name = '{userName}' AND password = '{oldPass}'";
                _command.ExecuteScalar();
                if (CheckPassword(userName, newPass))
                    return true;
                else
                    return false;
            }
        }

        public void AddUser(string name, string pass)
        {
            name = name.ToLower().Trim(new char[] { '\r', '\n', '\0' });
            OpenConnection();
            if (checkForTableExist(_tableUsers))
            {
                try
                {
                    lock (_command)
                    {
                        _command.CommandText = $"INSERT INTO {_tableUsers} (user_name, password)" +
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

            lock (_command)
            {
                _command.CommandText = $"SELECT * FROM {_tableUsers} WHERE user_name = '{userNameToLower}'";
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

        public void UpdateLoginStatus(Account account)
        {
            OpenConnection();
            lock (_command)
            {
                if (account.IsLogged)
                {
                    _command.CommandText = $"UPDATE {_tableUsers} SET isLogged = '0' WHERE id='{account.Id}'";
                    account.IsLogged = false;
                }
                else
                {
                    _command.CommandText = $"UPDATE {_tableUsers} SET isLogged = '1' WHERE id='{account.Id}'";
                    account.IsLogged = true;
                }
                _command.ExecuteNonQuery();
            }
        }

        string CheckNull(string[] data, int position)
        {

            for (int i = position + 1; i < data.Length; i++)
                if (!StringIsNull(data[i]))
                    return ",";
            return "";
        }

        bool DataIsNull(string[] data)
        {
            foreach (var d in data)
                if (!StringIsNull(d))
                    return false;
            return true;
        }

        bool StringIsNull(string data)
        {
            if (data == "")
                return true;
            return false;
        }

        public void UpdateUserData(int id, string[] data)
        {
            OpenConnection();

            lock (_command)
            {
                _command.CommandText = $"UPDATE {_tableUsers} SET {((!StringIsNull(data[0])) ? "email = '" + data[0] + $"'{CheckNull(data, 0)}" : "")}" +
                    $"{ ((!StringIsNull(data[1])) ? " firstName = '" + data[1] + $"'{CheckNull(data, 1)}" : "")}" +
                    $"{ ((!StringIsNull(data[2])) ? " secondName = '" + data[2] + $"'{CheckNull(data, 2)}" : "")}" +
                    $"{ ((!StringIsNull(data[3])) ? " phone = '" + int.Parse(data[3]) + "'" : "")}  WHERE id={id}";

                if (!DataIsNull(data))
                    _command.ExecuteScalar();
            }
        }

        public string GetAllLogedUser()
        {
            OpenConnection();
            string users = "";


            lock (_command)
            {
                _command.CommandText = $"SELECT * FROM {_tableUsers} WHERE isLogged = '1'";
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
    }
}
