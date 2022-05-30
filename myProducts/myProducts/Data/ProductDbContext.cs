using Microsoft.EntityFrameworkCore;
using myProducts.Models;

namespace myProducts.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "XBOX", Price = 1000M, Description = "The best gaming console" },
                new Product { Id = 2, Name = "PS 5", Price = 955.56M, Description = "The best console ever" },
                new Product { Id = 3, Name = "Nintendo", Price = 2500M, Description = "Nintendo Switch" },
                new Product { Id = 4, Name = "Playstation 4", Price = 1250M, Description = "The best console ever" },
                new Product { Id = 5, Name = "Wii U", Price = 500M, Description = "The best console ever" },
                new Product { Id = 6, Name = "Wii", Price = 500M, Description = "The best console ever" }
            );
        }



    }
}
