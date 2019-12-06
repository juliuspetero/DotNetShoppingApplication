using ShoppingApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class SQLTransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext context;

        public SQLTransactionRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Transaction CreateTransaction(Transaction newTransaction)
        {
            throw new NotImplementedException();
        }

        public Transaction DeleteTransaction(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public int GetNumberTransactions()
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransactionById(string id)
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransactionByRequestId(string requestId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetTransactionsByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Transaction UpdateTransaction(Transaction transactionChanges)
        {
            throw new NotImplementedException();
        }
    }
}
