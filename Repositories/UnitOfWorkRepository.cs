using Microsoft.Extensions.Configuration;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private ITransactionRepository transactionRepository;
        private IAccountRepository accountRepository;
        private IPaymentProviderRepository paymentProviderRepository;


        public UnitOfWorkRepository(AppDbContext context,
                                    IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                return orderRepository = orderRepository ?? new SQLOrderRepository(context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return productRepository = productRepository ?? new SQLProductRepository(context);
            }
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                return transactionRepository = transactionRepository ?? new SQLTransactionRepository(context);
            }
        }

        public IPaymentProviderRepository PaymentProviderRepository
        {
            get
            {
                return paymentProviderRepository = paymentProviderRepository ?? new SQLPaymentProviderRepository(context);
            }
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                return accountRepository = accountRepository ?? new SQLAccountRepository(context, configuration);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
