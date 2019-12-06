using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class OrderProduct
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}       
