using InventoryControl.API.Requests;
using InventoryControl.Core.DTO;

namespace InventoryControl.API.Extensions
{
    public static class CreateMovementRequestExtension
    {
        public static MovementDTO ToDTO(this CreateMovementRequest request)
        {
            return new MovementDTO
            {
                ProductCode = request.ProductCode,
                Quantity = request.Quantity,
                Type = request.Type
            };
        }
    }
}
