namespace InventoryControl.API.Responses
{
    public class MovementResponse
    {
        public string Message { get; set; } = null!;
        public int MovementId { get; set; }
        public int Balance { get; set; }
    }
}
