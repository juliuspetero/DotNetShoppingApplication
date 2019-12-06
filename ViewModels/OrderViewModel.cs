using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.ViewModels
{
    public class OrderViewModel
    {
        public string TotalAmount { get; set; }
        public string PhoneNumber { get; set; }
        public List<Product> OrderProducts { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
