using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public TransactionsController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }


        // Get all transactions
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Transaction> transactions = unitOfWorkRepository.TransactionRepository.GetAllTransactions();

            if (transactions == null)
            {
                return NotFound(new { status = "No transactions to display" });
            }

            return Ok(transactions);
        }

        // GET Transaction by a particular ID
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            Transaction transaction = unitOfWorkRepository.TransactionRepository.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound(new { status = $"Transaction with ID = {id} is not found" });
            }

            return Ok(transaction);

        }

        // Get transaction by the request ID
        [Route("requestid")]
        [HttpGet("{id}")]
        public ActionResult GetByRequestId(string id)
        {
            Transaction transaction = unitOfWorkRepository.TransactionRepository.GetTransactionByRequestId(id);

            if (transaction == null)
            {
                return NotFound(new { status = $"Transaction with Request ID = {id} is not found" });
            }

            return Ok(transaction);

        }

        // Create a transaction
        [HttpPost]
        public ActionResult Post([FromBody] Transaction transaction)
        {
            try
            {
                Transaction newTransaction = unitOfWorkRepository.TransactionRepository.CreateTransaction(transaction);
                return Ok(newTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // Update Transaction
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Transaction transaction)
        {
            try
            {
                // Get the transaction with that specified ID
                if (unitOfWorkRepository.TransactionRepository.GetTransactionById(id) == null)
                {
                    return NotFound(new { status = $"Transaction with ID = {id} is not found" });
                }

                Transaction updatedTransaction = unitOfWorkRepository.TransactionRepository.UpdateTransaction(transaction);
                return Ok(updatedTransaction);
            }
            catch (Exception ex)
            {

                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Transaction deletedTransaction = unitOfWorkRepository.TransactionRepository.DeleteTransaction(id);
            if (deletedTransaction != null)
            {
                return Ok(deletedTransaction);
            }

            return BadRequest(new { errorMessage = $"Transaction with ID = {id} is not found!!" });
        }
    }
}
