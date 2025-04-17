namespace InventoryControl.Core;

public static class Constants
{
    public const int MaxProductCodeLength = 50;

    public static class Messages
    {
        public const string ProductNotFound = "Product not found.";
        public const string InsufficientStock = "Insufficient stock.";
        public const string InvalidMovementType = "Invalid movement type.";
        public const string UnexpectedError = "Unexpected error.";
    }

    public static class MovementTypes
    {
        public const string Inbound = "Inbound";
        public const string Outbound = "Outbound";
    }
}
