using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Interfaces
{
    public interface IPaymentProviderRepository
    {
        List<PaymentProvider> GetAllPaymentProviders();
        List<PaymentProvider> UpdatePaymentProviders();
        PaymentProvider GetPaymentProviderById(string paymentId);
        PaymentProvider DeletePaymentProvider(string paymentId);
    }
}
