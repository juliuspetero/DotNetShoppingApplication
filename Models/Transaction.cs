using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RequestId { get; set; }
        public string CreatedOn { get; set; }
        public string Status { get; set; }
        public string BatchId { get; set; }
        public string Amount { get; set; }
        public string PaymentProvider { get; set; }
        public string OrderId { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
