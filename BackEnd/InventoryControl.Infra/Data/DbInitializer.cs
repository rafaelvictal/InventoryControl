using InventoryControl.Domain.Models;

namespace InventoryControl.Infra.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Mechanical Keyboard", Code = "KEYB01" },
                    new Product { Name = "Gaming Mouse", Code = "MOUSE02" },
                    new Product { Name = "Monitor 27\"", Code = "MON27" },
                    new Product { Name = "Laptop i7", Code = "LAPTOP04" }
                );
                context.SaveChanges();
            }
        }
    }
}
