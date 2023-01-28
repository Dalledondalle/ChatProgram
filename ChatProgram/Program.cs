using ChatProgram.Server;
using System;
using System.Net;
Console.Title = "SERVER";
IPAddress myIP = IPAddress.Parse("127.0.0.1");
int port = 8080;

Server server = new(myIP, port);

server.startListening();
Console.WriteLine("Server Started!");

Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("Waiting for connection");

server.acceptClient();
Console.WriteLine("Client Connected");



try
{
    server.clientData();
    while (server.ServerStatus)
    {
        string messageFromClient = string.Empty;
        string messageToClient = string.Empty;
        messageFromClient = server.streamReader.ReadLine();
        Console.WriteLine("Client: " + messageFromClient);
        if (messageFromClient.ToLower() == "exit")
        {
            server.disconnect();
            continue;
        }

        Console.Write("Server : ");
        messageToClient = Console.ReadLine();
        server.streamWriter.WriteLine(messageToClient);
        server.streamWriter.Flush();
    }
    server.close();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}



