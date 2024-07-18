using Microsoft.EntityFrameworkCore;
using ProductApp.Models;
using static System.Net.WebRequestMethods;

namespace ProductApp.Context
{
    public class ProductAppDbContext : DbContext 
    {
        public ProductAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Mobile",
                    Price = "10000",
                    ImageUrl = "https://storagebharath.blob.core.windows.net/productapp/mobile.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Price = "200000",
                    ImageUrl = "https://storagebharath.blob.core.windows.net/productapp/laptop.jpg"
                }
                );
        }
    }
}
