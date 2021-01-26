using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class Connection
    {
        IPAddress _ip;
        int _port;

        TcpClient client;
        NetworkStream stream;

        byte[] buffer;
        public int bufferSize = 1024;
        public Connection(string ip, string port)
        {
            _ip = IPAddress.Parse(ip);
            _port = int.Parse(port);
            buffer = new byte[bufferSize];
            client = new TcpClient();
            client.Connect(new IPEndPoint(_ip, _port));
            stream = client.GetStream();
        }

        public NetworkStream Stream { get => stream; set => stream = value; }
        public TcpClient Client { get => client; set => client = value; }
        public byte[] Buffer { get => buffer; set => buffer = value; }
    }
}
