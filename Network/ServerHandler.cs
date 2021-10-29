using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataServerRestaurant.Model;
using DataServerRestaurant.Persistence;

namespace DataServerRestaurant.Network
{
    public class ClientHandler
    {
        private TcpClient client;
        private IOrdersPersistence persistence;

        private StreamWriter writer;
        private StreamReader reader;

        private bool clientConnected;

        private JsonSerializerOptions options;

        public ClientHandler(TcpClient client, PersistenceRouter persistence)
        {
            this.client = client;
            this.persistence = persistence.Orders;
            
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            NetworkStream stream = client.GetStream();
            writer = new StreamWriter(stream, Encoding.ASCII) {AutoFlush = true};
            reader = new StreamReader(stream, Encoding.ASCII);

        }

        public async Task Start()
        {
            clientConnected = true;

            do
            {
                try
                {
                    var requestType = reader.ReadLine();
                    await ProcessClientRequestType(requestType);
                }
                catch (IOException e)
                {
                    clientConnected = false;
                }
            } while (clientConnected);
            
            client.Close();
        }
        
        private async Task ProcessClientRequestType(string requestType)
        {
            switch (requestType)
            {
                case "getOrders":
                    await GetOrders();
                    break;
                case "createOrder":
                    await CreateOrder();
                    break;
            }
        }

        private async Task GetOrders()
        {
            string orderJson;
            try
            {
                orderJson = JsonSerializer.Serialize(await persistence.GetOrders(), options);
                writer.WriteLine(orderJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async Task CreateOrder()
        {
            string requestBody;
            requestBody = reader.ReadLine();
            Order order = JsonSerializer.Deserialize<Order>(requestBody, options);
            await persistence.CreateOrder(order);
            string orderJson = JsonSerializer.Serialize(order, options);
            writer.WriteLine(orderJson);
        }

    }
}