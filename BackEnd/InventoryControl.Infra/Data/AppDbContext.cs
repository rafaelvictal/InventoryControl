using InventoryControl.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<StockMovement> Movements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
        }
    }
}
