using System.Collections.Generic;
using System.Threading.Tasks;
using DataServerRestaurant.Model;
using Microsoft.EntityFrameworkCore;

namespace DataServerRestaurant.Persistence
{
    public class OrdersPersistence : IOrdersPersistence
    {
        private RestaurantDbContext dbContext;

        public OrdersPersistence(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            dbContext.Entry(order).State = EntityState.Added;
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await dbContext.Orders.Include(o => o.Customer).Include(o => o.Menu).ToListAsync();
        }
    }
}