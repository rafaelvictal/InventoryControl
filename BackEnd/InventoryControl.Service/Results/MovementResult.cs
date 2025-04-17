namespace InventoryControl.Core.Results;

public class MovementResult
{
    public bool IsSuccess { get; set; }
    public int MovementId { get; set; }
    public int Quantity { get; set; }
    public string Message { get; set; }

    public MovementResult(bool isSucess, int movementId, int quantity, string message)
    {
        IsSuccess = isSucess;
        MovementId = movementId;
        Quantity = quantity;
        Message = message;
    }

    public static MovementResult Sucess(int movementId, int quantity) =>
        new MovementResult(true, movementId, quantity, "Movement registered successfully.");

    public static MovementResult Fail(string message) => new MovementResult(false, 0, 0, message);
}
