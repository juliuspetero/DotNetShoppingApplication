using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using ShoppingApplication.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{

    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProductsController(IUnitOfWorkRepository unitOfWorkRepository, 
                                  IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // Get all products in the database
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
           IEnumerable<Product> products = unitOfWorkRepository.ProductRepository.GetAllProducts();

            if (products == null)
            {
                return NotFound(new { status = "No product to display" });
            }

            return Ok(products);
        }

        // Get a specific product by ID
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(string id)
        {
            Product product = unitOfWorkRepository.ProductRepository.GetProduct(id); 

            if (product == null)
            {
                return NotFound(new { status = $"Product with ID = {id} is not found" });
            }

            return Ok(product);

        }

        // Create a new product
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {
            try
            {
                var formRequest = Request.Form;

                string uniqueFileName = "noimage.png";

                if (formRequest.Files.Count() > 0)
                {
                    uniqueFileName = ProcessUploadedFile(formRequest.Files[0]);
                }

                // Create the product model
                Product newProduct = new Product
                {
                    Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "").Replace("/", ""),
                    PhotoPath = uniqueFileName,
                    Name = formRequest["Name"],
                    Description = formRequest["Description"],
                    Price = Convert.ToDouble(formRequest["Price"])
                };

                newProduct = unitOfWorkRepository.ProductRepository.Add(newProduct);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {

                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // Update a given product
        [HttpPut("{id}")]
        public ActionResult Put(string id)
        {
            // Get the product with that specified ID
            Product product = unitOfWorkRepository.ProductRepository.GetProduct(id);

            if (product == null)
            {
                return NotFound(new { status = $"Product with ID = {id} is not found" });
            }

            try
            {
                var formRequest = Request.Form;

                if (formRequest.Files.Count() > 0)
                {
                    if (product.PhotoPath != "noimage.png")
                    {
                        // The user is sending some file, delete the previous photo path
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", product.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    // Upload the photo and update its DB name
                    product.PhotoPath = ProcessUploadedFile(formRequest.Files[0]);
                    
                }

                if (formRequest["Name"].Count() > 0){
                    product.Name = formRequest["Name"];
                }

                if (formRequest["Price"].Count() > 0)
                {
                    product.Price = Convert.ToDouble(formRequest["Price"]);
                }

                if (formRequest["Description"].Count() > 0)
                {
                    product.Description = formRequest["Description"];
                }

                // Update the product
                product = unitOfWorkRepository.ProductRepository.Update(product);

                return Ok(product);


            }
            catch (Exception ex )
            { 
                return BadRequest(new
                {
                    errorMessage = ex.Message
                });
            }


        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public  ActionResult Delete(string id)
        {
           Product deletedProduct = unitOfWorkRepository.ProductRepository.Delete(id);
            if (deletedProduct != null)
            {
                // Delete the photo as well
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", deletedProduct.PhotoPath);
                System.IO.File.Delete(filePath);

                return Ok(deletedProduct);
            }

            return BadRequest(new { errorMessage = $"Product with ID = {id} is not found!!" });
            
        }

        // This generate a file path and save the photo to a file
        private string ProcessUploadedFile(IFormFile formFile)
        {
            string uniqueFileName = null;

            if (formFile != null)    
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
