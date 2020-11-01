using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public class Account
    {
        public int? id { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public bool isLogged { get; set; }

        Account(int? id, string login, string pass, bool isLogged)
        {
            this.id = id;
            this.login = login;
            this.pass = pass;
            this.isLogged = isLogged;
        }



    }
}
