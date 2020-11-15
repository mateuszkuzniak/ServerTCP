﻿using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommProtocolLibrary
{
    public abstract class TcpServer
    {
        IPAddress ipAdress;
        int port;
        int buffer_size = 1024;
        bool running;
        TcpListener tcpListener;
        TcpClient tcpClient;
        protected NetworkStream stream;
        protected DatabaseUser _usersDatabase;
        protected DatabaseFile _filesDatabase;



        public IPAddress IPAddress
        {
            get => ipAdress;
            set
            {
                if (!running)
                    ipAdress = value;
                else
                    throw new Exception("IP Address can't be changed while server is running");
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
                    throw new Exception("Port can't be changed while server is running");

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
                    throw new Exception("Packet size can't be changed while server is running");

            }
        }

        protected TcpListener TcpListener { get => tcpListener; set => tcpListener = value; }
        protected TcpClient TcpClient { get => tcpClient; set => tcpClient = value; }
        protected NetworkStream Stream { get => stream; set => stream = value; }

        protected bool checkPort()
        {
            if (port > 1024 && port < 49151)
                return true;
            return false;
        }

        public TcpServer(IPAddress ip, int port)
        {
            _usersDatabase = new DatabaseUser("users","users");
            _filesDatabase = new DatabaseFile("users", "files");

            running = false;
            IPAddress = ip;
            Port = port;
            if (!checkPort())
            {
                Port = 8000;
                throw new Exception("Invalid value of port, port set to 8000 ");
            }
        }

        protected void StartListening()
        {
            TcpListener = new TcpListener(IPAddress, Port);
            TcpListener.Start();
        }

        /// <summary>
        /// Funkcja odpowiedzialan za wysyłanie wiadomości do klienta
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        protected void WriteMessage(NetworkStream stream, byte[] message)
        {
            stream.Write(message, 0, message.Length);
        }

        /// <summary>
        /// Funkcja odpowiedzialna za odbieranie wiadomości od klienta
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected string ReadMessage(NetworkStream stream)
        {
            string message;
            int size;
            byte[] reciveBuffer = new byte[BufferSize];

            size = stream.Read(reciveBuffer, 0, reciveBuffer.Length);
            if (reciveBuffer[0] == 13 && reciveBuffer[1] == 10)
                size = stream.Read(reciveBuffer, 0, reciveBuffer.Length);
            

            return message = Encoding.UTF8.GetString(reciveBuffer, 0, size);
        }

        protected abstract void AcceptClient();
        protected abstract void BeginDataTransmission(NetworkStream stream);
        public abstract void Start();

    }
}
