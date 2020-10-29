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
    }
}
