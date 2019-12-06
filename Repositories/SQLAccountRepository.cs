using Microsoft.Extensions.Configuration;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Repositories
{
    public class SQLAccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;
        public IConfiguration Configuration { get; }

        public SQLAccountRepository(AppDbContext context,
                                    IConfiguration configuration)
        {
            this.context = context;
            Configuration = configuration;
        }

        public Account GetAccountDetails()
        {
            string accountId = Configuration["XenteConnection:AccountId"];
            return context.Accounts.Find(accountId);
        }

        // Make call to the Xente API
        public Account UpdateAccountDetails()
        {
            throw new NotImplementedException();
        }


        // Update the local DB
        public Account UpdateAccountDetails(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
