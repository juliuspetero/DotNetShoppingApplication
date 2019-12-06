using ShoppingApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public SQLOrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Order CreateOrder(Order newOrder)
        {
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder;
        }

        public Order DeleteOrder(string id)
        {
            Order order = context.Orders.Find(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();

            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders;
        }

        public int GetNumberOfOrders()
        {
            return context.Orders.Count();
        }

        public Order GetOrderById(string id)
        {
            return context.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            yield return context.Orders.FirstOrDefault(o => o.UserId == userId);
        }

        public Order UpdateOrder(Order orderChanges)
        {
            var order = context.Orders.Attach(orderChanges);
            order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return orderChanges;
        }
    }
}
