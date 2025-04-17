using InventoryControl.Core.DTO;
using InventoryControl.Core.Services;
using InventoryControl.Domain.Models;
using InventoryControl.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace InventoryControl.Test
{
    public class InventoryServiceTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=:memory:")
                .Options;

            var context = new AppDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task AddMovement_Should_Return_Error_When_Product_Not_Exists()
        {
            // Arrange
            var context = GetInMemoryContext();
            var logger = new NullLogger<InventoryService>();
            var service = new InventoryService(logger, context);

            var dto = new MovementDTO
            {
                ProductCode = "INVALID",
                Type = "Outbound",
                Quantity = 10
            };

            // Act
            var result = await service.AddMovementAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Product not found.", result.Message);
        }

        [Fact]
        public async Task AddMovement_Should_Succeed_When_Product_Exists()
        {
            // Arrange
            var context = GetInMemoryContext();
            var logger = new NullLogger<InventoryService>();
            var service = new InventoryService(logger, context);

            context.Products.Add(new Product { Name = "Test Product", Code = "TEST123" });
            context.SaveChanges();

            var dto = new MovementDTO
            {
                ProductCode = "TEST123",
                Type = "Inbound",
                Quantity = 5
            };

            // Act
            var result = await service.AddMovementAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.MovementId);
            Assert.Equal(5, result.Quantity);
        }
    }
}