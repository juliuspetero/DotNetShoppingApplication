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
    public class PaymentProvidersController : Controller
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public PaymentProvidersController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        // GET All Payment Providers
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<PaymentProvider> paymentProviders = unitOfWorkRepository.PaymentProviderRepository.GetAllPaymentProviders();

            if (paymentProviders == null)
            {
                return NotFound(new { status = "No Payment provider to display" });
            }

            return Ok(paymentProviders);
        }

        // GET Payment Provider by its ID
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            PaymentProvider paymentProvider = unitOfWorkRepository.PaymentProviderRepository.GetPaymentProviderById(id);

            if (paymentProvider == null)
            {
                return NotFound(new { status = $"Payment Provider with Payment ID = {id} is not found" });
            }

            return Ok(paymentProvider);
        }


        // Update payment providers list
        [HttpPut]
        public ActionResult Put()
        {
            try
            {
                IEnumerable<PaymentProvider> paymentProviders = unitOfWorkRepository.PaymentProviderRepository.UpdatePaymentProviders();
                return Ok(paymentProviders);

            }
            catch (Exception ex)
            {

                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // DELETE A payment provider
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            PaymentProvider deletedPaymentProvider = unitOfWorkRepository.PaymentProviderRepository.DeletePaymentProvider(id);
            if (deletedPaymentProvider != null)
            {
                return Ok(deletedPaymentProvider);
            }

            return BadRequest(new { errorMessage = $"Payment Provider with ID = {id} is not found!!" });
        }
    }
}
