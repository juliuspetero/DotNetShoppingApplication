using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(string id);
        Order CreateOrder(Order newOrder);
        Order UpdateOrder(Order orderChanges);
        Order DeleteOrder(string id);
        IEnumerable<Order> GetOrdersByUserId(string userId);
        int GetNumberOfOrders();
    }
}
