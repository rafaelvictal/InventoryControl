using InventoryControl.Core.DTO;
using InventoryControl.Core.Results;

namespace InventoryControl.Core.Interfaces;

public interface IInventoryService
{
    public Task<MovementResult> AddMovementAsync(MovementDTO dto);

    public Task<ReportResult> GenerateReportAsync(DateTime date, string? productCode);
}
