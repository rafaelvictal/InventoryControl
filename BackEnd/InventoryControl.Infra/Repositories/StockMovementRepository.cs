using InventoryControl.Domain.Models;
using InventoryControl.Infra.Data;
using InventoryControl.Infra.Interfaces;

namespace InventoryControl.Infra.Repositories;

public class StockMovementRepository : Repository<StockMovement>, IStockMovementRepository
{
    private readonly AppDbContext _dbContext;

    public StockMovementRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}