using InventoryControl.Infra.Interfaces;
using InventoryControl.Infra.Repositories;

namespace InventoryControl.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IProductRepository Product { get; private set; }

    public IStockMovementRepository StockMovement { get; private set; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Product = new ProductRepository(dbContext);
        StockMovement = new StockMovementRepository(dbContext);
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}