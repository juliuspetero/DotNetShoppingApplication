using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    // Creates all the tables related to authentication and authorization
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // Creates table in the database
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentProvider> PaymentProviders { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedEmployees();
            modelBuilder.SeedRoles();

            //Set the primary keys
            modelBuilder.Entity<Product>() 
                .HasKey(p => p.Id);
            modelBuilder.Entity<PaymentProvider>().HasKey(p => p.PaymentId);
            modelBuilder.Entity<Account>().HasKey(a => a.AccountId);
            #endregion

            // Create the OrderProduct entity with two composite keys
            modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>().HasOne(op => op.Order).WithMany(op => op.OrderProducts).HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderProduct>().HasOne(op => op.Product).WithMany(op => op.OrderProducts).HasForeignKey(o => o.ProductId);
        }
    }
}
