using InventoryControl.Domain.Models;
using InventoryControl.Infra.Data;
using InventoryControl.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetOneByCode(string code)
    {
        return await _dbContext.Products.SingleOrDefaultAsync(p => p.Code == code);
    }
}
