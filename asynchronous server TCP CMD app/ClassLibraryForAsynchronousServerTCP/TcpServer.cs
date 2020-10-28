using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForAsynchronousServerTCP
{
    public abstract class TcpServer
    {
        IPAddress ipAdress;
        int port;
        int buffer_size = 1024;
        bool running;
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream stream;

        public IPAddress IPAddress
        {
            get => ipAdress;
            set
            {
                if (!running)
                    ipAdress = value;
                else
                    throw new Exception("IP Address can't be changed while the server is powered on");
            }
        }

        public int Port
        {
            get => port;
            set
            {
                int temp = port;
                if (!running)
                    port = value;
                else
                    throw new Exception("Port can't be changed while the server is powered on");

                if (!checkPort())
                {
                    port = temp;
                    throw new Exception("Invalid port value");
                }
            }
        }

        public int BufferSize
        {
            get => buffer_size;
            set
            {
                if (value < 0 || value > 1024 * 1024 * 64)
                    throw new Exception("Invalide packet size");
                if (!running)
                    buffer_size = value;
                else
                    throw new Exception("Packet size can't be changed while the server is powered on");

            }
        }

        protected TcpListener TcpListener { get => tcpListener; set => tcpListener = value; }
        protected TcpClient TcpClient { get => tcpClient; set => tcpClient = value; }
        protected NetworkStream NetworkStream { get => stream; set => stream = value; }

        protected bool checkPort()
        {
            if (port > 1024 && port < 49151)
                return true;
            return false;
        }

        public TcpServer(IPAddress ip, int port)
        {
            running = false;
            IPAddress = ip;
            Port = port;
            if (!checkPort())
            {
                Port = 8000;
                throw new Exception("Invalid value of port, port was set to 8000 ");
            }
        }

        protected void StartListening()
        {
            TcpListener = new TcpListener(IPAddress, Port);
            TcpListener.Start();
        }

        protected abstract void AcceptClient();
        protected abstract void BeginDataTransmission(NetworkStream stream);
        public abstract void Start();

    }
}
