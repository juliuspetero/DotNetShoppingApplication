using ShoppingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(string id);

        IEnumerable<Product> GetAllProducts();

        Product Add(Product product);

        Product Update(Product productChanges);

        Product Delete(string id);

    }
}
