using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using ShoppingApplication.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{

    [Route("api/[controller]")]
    public class XenteAccountController : Controller
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public XenteAccountController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        // GET the account details
        [HttpGet]
        public ActionResult Get()
        {
            Account account = unitOfWorkRepository.AccountRepository.GetAccountDetails();

            if (account == null)
            {
                return NotFound(new { status = $"There is no account details to display!!" });
            }

            return Ok(account);
        }


        // Update the current account details
        [HttpPut]
        public ActionResult Put()
        {
            try
            {
                // Make call to the Xente API 

                Account updatedAccountDetails = unitOfWorkRepository.AccountRepository.UpdateAccountDetails();

                updatedAccountDetails = unitOfWorkRepository.AccountRepository.UpdateAccountDetails(updatedAccountDetails);
                return Ok(updatedAccountDetails);
            }
            catch (Exception ex)
            {

                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
