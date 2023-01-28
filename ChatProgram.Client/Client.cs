using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ChatProgram.Client
{

    public class Client
    {
        public IPAddress MyIP { get; private set; }
        public int Port { get; private set; }
        private TcpClient socketForServer;
        public bool clientStatus;
        public NetworkStream NetworkStream { get; private set; }
        public StreamWriter StreamWriter { get; private set; }
        public StreamReader StreamReader { get; private set; }

        public Client(IPAddress myIP, int port)
        {
            MyIP = myIP;
            Port = port;
        }

        public void ConnectToServer()
        {
            try
            {
                socketForServer = new TcpClient(MyIP.ToString(), Port);
            }
            catch (Exception ex)
            {

            }
            if(socketForServer.Connected)
                clientStatus = true;
        }

        public void ServerData()
        {
            NetworkStream = socketForServer.GetStream();
            StreamReader = new StreamReader(NetworkStream);
            StreamWriter = new StreamWriter(NetworkStream);
        }

        public void Disconnect()
        {
            StreamReader.Close();
            StreamWriter.Flush();
            StreamWriter.Close();   
            socketForServer.Close();
            NetworkStream.Close();
            clientStatus = false;
        }
    }
}
