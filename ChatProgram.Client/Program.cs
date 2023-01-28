using ChatProgram.Client;
using System;
using System.Threading;
using System.Net;

IPAddress myIP = IPAddress.Parse("127.0.0.1");
int port = 8080;
Thread myThread;

Client client = new Client(myIP, port);

//Dns.GetHostAddresses("www.dondalle.xyz");

while(!client.clientStatus)
{
    Console.Clear();
    client.ConnectToServer();
    Console.Write("Connecting");
    
    for(int i = 0; i < 5; i++)
    {
        Console.Write(".");
        Thread.Sleep(200);
    }    
}
client.ServerData();
Console.Clear();
try
{
    while (client.clientStatus)
    {
        string messageToServer = string.Empty;
        string messageFromServer = string.Empty;
        Console.Write("Client: ");
        messageToServer = Console.ReadLine();
        if (messageToServer.ToLower() == "exit")
        {
            client.Disconnect();
            Console.WriteLine("Bye");            
        }
        else
        {
            client.StreamWriter.WriteLine(messageToServer);
            client.StreamWriter.Flush();

            messageFromServer = client.StreamReader.ReadLine();
            Console.WriteLine("Server: " + messageFromServer);
        }

        
    }
}
catch (Exception e)
{

    Console.WriteLine(e.Message);
}
