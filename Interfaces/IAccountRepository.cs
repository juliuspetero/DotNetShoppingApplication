using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccountDetails();

        // Get from Xente at the current moment
        Account UpdateAccountDetails();

        // Save to the database
        Account UpdateAccountDetails(Account account);
    }
}
