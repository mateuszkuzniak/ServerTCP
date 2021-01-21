using DatabaseLibrary;
using MessageLibrary;

namespace ServerLibrary
{
    public abstract class CommunicationProtocol : Logger
    {
        public User UsersDatabase { get; set; }
        public FileDb FileDatabase { get; set; }

        protected CommunicationProtocol()
        {
        }
        public abstract string GenerateResponse(string message);
        public abstract bool GetUserStatus();
        public abstract void SetDatabaseUser(User database);
        public abstract void SetDatabaseFile(FileDb database);

        public abstract Account GetUser();

    }
}
