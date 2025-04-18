using InventoryControl.Domain.Models;

namespace InventoryControl.Infra.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    public Task<Product> GetOneByCode(string code);
}
