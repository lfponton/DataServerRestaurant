using System.Collections.Generic;
using System.Threading.Tasks;
using DataServerRestaurant.Model;

namespace DataServerRestaurant.Persistence
{
    public interface IOrdersPersistence
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetOrders();
    }
}