namespace InventoryControl.Core.DTO
{
    public record StockReportDTO
    {
        public string ProductName { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public int Inbound { get; set; }
        public int Outbound { get; set; }
        public int Balance => Inbound - Outbound;
    }
}
