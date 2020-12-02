using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Account
    {
        string _login;
        Password _password;

        public Account(string login, string password)
        {
            _login = login;
            _password = new Password(password);
        }

        public string Login { get => _login; set => _login = value; }
        public Password Password { get => _password; set => _password = value; }
    }
}
