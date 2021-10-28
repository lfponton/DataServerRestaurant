using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DataServerRestaurant.Persistence;

namespace DataServerRestaurant.Network
{
    public class Server
    {
        private IPAddress address;
        private int port;
        private TcpListener server;
        private PersistenceRouter persistence;

        public Server(IPAddress address, int port, PersistenceRouter persistence)
        {
            this.persistence = persistence;
            this.address = address;
            this.port = port;
        }

        public void Listen()
        {
            try
            {
                server = new TcpListener(address, port);
                
                server.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    //here we could start a different handler depending on the client connected
                    ClientHandler handler = new ClientHandler(client, persistence);
                    Thread thread = new Thread(h => handler.Start());
                    thread.Start();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}