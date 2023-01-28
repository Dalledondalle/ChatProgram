using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatProgram.Server
{
    public class Server
    {
        public IPAddress MyIP { get; private set; }
        public int Port { get; private set; }
        public bool ServerStatus { get; private set; }
        private TcpListener TcpListener { get; set; }
        public Socket SocketForClient { get; private set; }


        public NetworkStream networkStream { get; private set; }
        public StreamReader streamReader { get; private set; }
        public StreamWriter streamWriter { get; private set; }

        public Server(IPAddress myIP, int port)
        {
            this.MyIP = myIP;
            this.Port = port;
            ServerStatus = true;
        }

        public void startListening()
        {
            try
            {
                TcpListener = new TcpListener(MyIP, Port);
                TcpListener.Start();
            }
            catch
            {
                Console.WriteLine("Could not start");
            }
        }

        public void acceptClient()
        {
            try
            {
                SocketForClient = TcpListener.AcceptSocket();
            }
            catch
            {
                Console.WriteLine("Could not accept client");
            }
        }

        public void clientData()
        {
            networkStream = new NetworkStream(SocketForClient);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }

        public void disconnect()
        {
            ServerStatus = false;
            networkStream.Close();
            streamReader.Close();
            streamWriter.Close();            
        }

        public void close()
        {
            disconnect();
            SocketForClient.Close();
        }
    }
}
