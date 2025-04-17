namespace InventoryControl.Core.DTO
{
    public record MovementDTO
    {
        public string ProductCode { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
