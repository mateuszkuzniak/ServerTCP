using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;

namespace ServerLibrary
{
    public abstract class CommunicationProtocol
    {
        public User UsersDatabase { get; set; }
        public FileDb FileDatabase { get; set; }

        protected CommunicationProtocol()
        {

        }
        public abstract string GenerateResponse(string message);
        //public abstract void GetAccount();
        public abstract bool GetStatus();
        public abstract void SetDatabaseUser(User database);
        public abstract void SetDatabaseFile(FileDb database);

        public abstract Account GetUser();

    }
}
