using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShoppingApplication.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransactionById(string id);
        Transaction CreateTransaction(Transaction newTransaction);
        Transaction UpdateTransaction(Transaction transactionChanges);
        Transaction DeleteTransaction(string id);
        IEnumerable<Order> GetTransactionsByUserId(string userId);
        int GetNumberTransactions();
        Transaction GetTransactionByRequestId(string requestId);
    }
}
