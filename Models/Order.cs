using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string TotalAmount { get; set; }
        public string PhoneNumber { get; set; }
        //public List<Product> OrderProducts { get; set; }
        public string DeliveryAddress { get; set; }
        public string PlaceOn { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public string UserId { get; set; }


        public List<OrderProduct> OrderProducts { get; set; }
    }
}
