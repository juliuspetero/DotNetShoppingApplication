using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class PaymentProvider
    {
        public string PaymentItemId { get; set; }
        public string Name { get; set; }
        public string PaymentRegex { get; set; }
        public string PaymentRegexStart { get; set; }
        public string PaymentId { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUri { get; set; }
    }
}
