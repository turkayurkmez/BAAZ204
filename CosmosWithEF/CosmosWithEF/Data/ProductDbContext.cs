using CosmosWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosWithEF.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseCosmos("https://turka-cosmos.documents.azure.com:443/", "TwT2LpE5hJmvDrViEnpsEijKd5doXnCTWWtp8JB0bEvGcXF8f0DMv6RvL1jAbvp10jzt5Kiem7Qfne6EuvecVA==", databaseName: "Products");
        }


        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Clothing");
            //modelBuilder.Entity<Product>().HasPartitionKey(o => o.PartitionKey);
        }
    }
}
