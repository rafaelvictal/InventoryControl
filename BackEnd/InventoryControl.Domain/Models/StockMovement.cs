namespace InventoryControl.Domain.Models;

public class StockMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string Type { get; set; } = null!; // Inbound | Outbound
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }
}
