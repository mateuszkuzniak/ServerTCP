using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public class Account
    {
        private int? id;
        private string login;
        private string pass;
        private bool isLogged;

        public int? Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Pass { get => pass; set => pass = value; }
        public bool IsLogged { get => isLogged; set => isLogged = value; }

        /// <summary>
        /// Funkcja czyszcząca obiekt
        /// </summary>
        public void Clear()
        {
            Id = null;
            Login = null;
            Pass= null;
            IsLogged = false;

        }
    }
}
