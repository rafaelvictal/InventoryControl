namespace InventoryControl.Infra.Interfaces;

public interface IUnitOfWork
{
    IStockMovementRepository StockMovement { get; }

    IProductRepository Product { get; }

    Task SaveChanges();
}
