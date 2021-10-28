using System.Collections.Generic;
using System.Threading.Tasks;
using DataServerRestaurant.Model;

namespace DataServerRestaurant.Persistence
{
    public class PersistenceRouter
    {
        public IOrdersPersistence Orders { get; private set; }

        public PersistenceRouter()
        {
            RestaurantDbContext dbContext = new RestaurantDbContext();
            Orders = new OrdersPersistence(dbContext);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            return await Orders.CreateOrder(order);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await Orders.GetOrders();
        }
    }
}