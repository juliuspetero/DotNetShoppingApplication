using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }       
        public string PhotoPath { get; set; }
        public string Description { get; set; }

        public string OrderId { get; set; }


        public List<OrderProduct>  OrderProducts { get; set; }
    }
}       
    