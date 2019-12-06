using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class Account
    {
        public string CreatedOn { get; set; }
        public string modifiedOn { get; set; }
        public string AccountId { get; set; }
        public string SubscriptionId { get; set; }
        public string AccountType { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryCode { get; set; }
        public string AccountStatus { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AlertLevel { get; set; }
        public string AlertChannel { get; set; }
        public string AlertEmailAddress { get; set; }
        public string AlertPhoneNumber { get; set; }
        public string CallBackUri { get; set; }
        public double Balance { get; set; }
        public double AccountPackage { get; set; }
    }
}
