using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public static class ModelBuilderExtension
    {
        // This method becomes one of the method in the ModelBuilder class
        public static void SeedEmployees(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                   Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "") ,
                   Name = "Laptop Bag",
                   Price = 1000,
                   PhotoPath = "laptopbag.jpg",
                   Description = "Good Laptop Bag"
                },
                new Product
                {
                    Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", ""),
                    Name = "Water Bottle",
                    Price = 1200,
                    PhotoPath = "waterbottle.jpg",
                    Description = "Good Water Bottle"
                },
                new Product
                {
                    Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", ""),
                    Name = "Red Sox Hat",
                    Price = 1000,
                    PhotoPath = "redsox.jpg",
                    Description = "Good Red Sox"
                }
                );
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new {Id = "1", Name = "Admin", NormalizedName = "ADMIN"},
                new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}
