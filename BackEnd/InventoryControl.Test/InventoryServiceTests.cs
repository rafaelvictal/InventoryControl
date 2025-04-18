using InventoryControl.Core.DTO;
using InventoryControl.Core.Services;
using InventoryControl.Domain.Models;
using InventoryControl.Infra.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Linq.Expressions;

namespace InventoryControl.Test
{
    public class InventoryServiceTests
    {
        [Fact]
        public async Task AddMovement_Should_Return_Error_When_Product_Not_Exists()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();

            uowMock.Setup(u => u.Product.GetOneByCode(It.IsAny<string>()))
                    .ReturnsAsync((Product?)null);

            var logger = new NullLogger<InventoryService>();
            var service = new InventoryService(logger, uowMock.Object);

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
            var product = new Product { Id = 1, Name = "Test Product", Code = "TEST123" };

            var uowMock = new Mock<IUnitOfWork>();

            uowMock.Setup(u => u.Product.GetOneByCode(It.IsAny<string>()))
                    .ReturnsAsync(product);

            uowMock.Setup(u => u.StockMovement.GetAll(It.IsAny<Expression<Func<StockMovement, bool>>>(), null))
                    .ReturnsAsync(new List<StockMovement>());

            uowMock.Setup(u => u.StockMovement.Add(It.IsAny<StockMovement>()));
            uowMock.Setup(u => u.SaveChanges()).Returns(Task.CompletedTask);

            var logger = new NullLogger<InventoryService>();
            var service = new InventoryService(logger, uowMock.Object);

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
            Assert.Equal(5, result.Quantity);
        }
    }
}
