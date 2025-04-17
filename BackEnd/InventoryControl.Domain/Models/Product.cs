namespace InventoryControl.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();
}
