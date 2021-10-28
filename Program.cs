using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DataServerRestaurant.Network;
using DataServerRestaurant.Persistence;

namespace DataServerRestaurant
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PersistenceRouter persistence = new PersistenceRouter();

            Server server = new Server(IPAddress.Parse("127.0.0.1"), 2001, persistence);
            var serverThread = new Thread(server.Listen);
            serverThread.Start();
            Console.WriteLine($"Server started.");

        }
    }
}