using InventoryControl.API.Responses;
using InventoryControl.Core.Results;

namespace InventoryControl.API.Extensions;

public static class MovementResultExtension
{
    public static MovementResponse toResponse(this MovementResult result)
    {
        return new MovementResponse
        {
            Balance = result.Quantity,
            MovementId = result.MovementId,
            Message = result.Message
        };
    }
}
