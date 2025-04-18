using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using InventoryControl.API;
using InventoryControl.Domain.Models;
using InventoryControl.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryControl.Test
{
    public class MovementsApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public MovementsApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Movement_Should_Return_BadRequest_When_Product_Invalid()
        {
            // Arrange
            var payload = new
            {
                productCode = "INVALID",
                type = "Outbound",
                quantity = 5
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/movements", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Movement_Should_Succeed_When_Product_Exists()
        {
            // Arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var code = $"API-{Guid.NewGuid().ToString().Substring(0, 8)}";
            db.Products.Add(new Product { Name = "API Test Product", Code = code });
            db.SaveChanges();

            var payload = new
            {
                productCode = code,
                type = "Inbound",
                quantity = 10
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/movements", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("Movement registered successfully", body);
        }
    }
}