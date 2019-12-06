using ShoppingApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
       

        public SQLProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(string id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();

            }
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products;
        }

        public Product GetProduct(string id)
        {
         
            return context.Products.Find(id);
        }

        public Product Update(Product productChanges)
        {
            var employee = context.Products.Attach(productChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }
    }
}
