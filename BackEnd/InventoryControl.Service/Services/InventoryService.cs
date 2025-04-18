using InventoryControl.Domain.Models;
using InventoryControl.Core.DTO;
using InventoryControl.Core.Extensions;
using InventoryControl.Core.Results;
using InventoryControl.Core.Interfaces;
using Microsoft.Extensions.Logging;
using InventoryControl.Infra.Interfaces;

namespace InventoryControl.Core.Services;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly IUnitOfWork _uow;

    public InventoryService(ILogger<InventoryService> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public async Task<MovementResult> AddMovementAsync(MovementDTO dto)
    {
        try
        {
            var product = await _uow.Product.GetOneByCode(dto.ProductCode);

            if (product == null) return MovementResult.Fail(Constants.Messages.ProductNotFound);

            if (dto.Type == Constants.MovementTypes.Outbound)
            {
                var balance = await CalculateBalanceAsync(product.Id);

                if (dto.Quantity > balance)
                    return MovementResult.Fail(Constants.Messages.InsufficientStock);
            }

            var movement = dto.toModel(product.Id);
            await _uow.StockMovement.Add(movement);
            await _uow.SaveChanges();

            return MovementResult.Sucess(movement.Id, movement.Quantity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return MovementResult.Fail(Constants.Messages.UnexpectedError);
        }
    }

    public async Task<ReportResult> GenerateReportAsync(DateTime date, string? productCode)
    {
        try
        {
            var products = string.IsNullOrEmpty(productCode)
                ? await _uow.Product.GetAll(includeProperties: "Movements")
                : await _uow.Product.GetAll(p => p.Code == productCode, includeProperties: "Movements");

            var stockReport = products.Select(p => new StockReportDTO
            {
                ProductName = p.Name,
                ProductCode = p.Code,
                Inbound = p.Movements
                    .Where(m => m.Type == Constants.MovementTypes.Inbound && m.CreatedAt.Date == date.Date)
                    .Sum(m => m.Quantity),
                Outbound = p.Movements
                    .Where(m => m.Type == Constants.MovementTypes.Outbound && m.CreatedAt.Date == date.Date)
                    .Sum(m => m.Quantity)
            }).ToList();

            return ReportResult.Sucess(stockReport);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return ReportResult.Fail(Constants.Messages.UnexpectedError);
        }
    }

    private async Task<int> CalculateBalanceAsync(int productId, DateTime? until = null)
    {
        var query = await _uow.StockMovement.GetAll(m => m.ProductId == productId);

        if (until.HasValue)
            query = query.Where(m => m.CreatedAt.Date <= until.Value.Date);

        var inbound = query.Where(m => m.Type == Constants.MovementTypes.Inbound).Sum(m => m.Quantity);
        var outbound = query.Where(m => m.Type == Constants.MovementTypes.Outbound).Sum(m => m.Quantity);

        return inbound - outbound;
    }
}
