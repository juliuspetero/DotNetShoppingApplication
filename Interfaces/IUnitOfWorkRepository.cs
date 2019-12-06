using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IOrderRepository OrderRepository { get; }

        IProductRepository ProductRepository { get; }

        ITransactionRepository TransactionRepository { get; }
        IAccountRepository AccountRepository { get; }
        IPaymentProviderRepository PaymentProviderRepository { get; }
            
        void Save();
    }
}
