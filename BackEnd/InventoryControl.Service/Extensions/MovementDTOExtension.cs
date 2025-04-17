using InventoryControl.Core.DTO;
using InventoryControl.Domain.Models;

namespace InventoryControl.Core.Extensions
{
    public static  class MovementDTOExtension
    {
        public static StockMovement toModel(this MovementDTO dto, int productId)
        {
            return new StockMovement
            {
                ProductId = productId,
                Type = dto.Type,
                Quantity = dto.Quantity,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
