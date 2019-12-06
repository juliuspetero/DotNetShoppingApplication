using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Repositories
{
    public class SQLPaymentProviderRepository : IPaymentProviderRepository
    {
        private readonly AppDbContext context;

        public SQLPaymentProviderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public PaymentProvider DeletePaymentProvider(string paymentId)
        {
            throw new NotImplementedException();
        }

        public List<PaymentProvider> GetAllPaymentProviders()
        {
            throw new NotImplementedException();
        }

        public PaymentProvider GetPaymentProviderById(string paymentId)
        {
            throw new NotImplementedException();
        }

        public List<PaymentProvider> UpdatePaymentProviders()
        {
            throw new NotImplementedException();
        }
    }
}
