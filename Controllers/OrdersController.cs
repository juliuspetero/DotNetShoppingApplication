using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models;
using ShoppingApplication.Repositories;
using ShoppingApplication.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly UnitOfWorkRepository unitOfWorkRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(UnitOfWorkRepository unitOfWorkRepository,
                                UserManager<ApplicationUser> userManager)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.userManager = userManager;
        }

        // Get all the orders
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Order> orders = unitOfWorkRepository.OrderRepository.GetAllOrders();

            if (orders == null)
            {
                return NotFound(new { status = "No orders to display" });
            }
            return Ok(orders);
        }

        // GET order by a particular ID
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            Order order = unitOfWorkRepository.OrderRepository.GetOrderById(id);

            if (order == null)
            {
                return NotFound(new { status = $"Order with ID = {id} is not found" });
            }

            return Ok(order);
        }

        // Create an order
        [HttpPost]
        public ActionResult Post([FromBody] OrderViewModel model)
        {
            // Create Transaction from Xente first
            Transaction createdTransaction = new Transaction
            {
                
            };

            Order order = new Order
            {
                Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "").Replace("/", ""),
                TotalAmount = model.TotalAmount,
                PhoneNumber = model.PhoneNumber,
                DeliveryAddress = model.DeliveryAddress,
                PlaceOn = createdTransaction.CreatedOn,
                PaymentStatus = createdTransaction.Status,
                TransactionId = createdTransaction.Id,
                UserId = userManager.GetUserId(HttpContext.User)
            };

            try
            {
                Order newOrder = unitOfWorkRepository.OrderRepository.CreateOrder(order);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // Udate the order by an ID
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] OrderViewModel model)
        {
            try
            {
                // Get the transaction with that specified ID
                Order order = unitOfWorkRepository.OrderRepository.GetOrderById(id);
                if ( order == null)
                {
                    return NotFound(new { status = $"Order with ID = {id} is not found" });
                }

                if (model.DeliveryAddress != null){
                    order.DeliveryAddress = model.DeliveryAddress;
                }

                if (model.OrderProducts !=null)
                {
                    //order.OrderProducts = model.OrderProducts;
                }

                if (model.PhoneNumber != null)
                {
                    order.PhoneNumber = model.PhoneNumber;
                }

                if (model.TotalAmount != null)
                {
                    order.TotalAmount = model.TotalAmount;
                }
                Order updatedOrder = unitOfWorkRepository.OrderRepository.UpdateOrder(order);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {

                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // Deletea specified order
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Order deletedOrder = unitOfWorkRepository.OrderRepository.DeleteOrder(id);
            if (deletedOrder != null)
            {
                return Ok(deletedOrder);
            }

            return BadRequest(new { errorMessage = $"Order with ID = {id} is not found!!" });
        }
    }
}
