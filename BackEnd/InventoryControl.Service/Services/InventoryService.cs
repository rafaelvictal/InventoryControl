using InventoryControl.Domain.Models;
using InventoryControl.Core.DTO;
using InventoryControl.Infra.Data;
using Microsoft.EntityFrameworkCore;
using InventoryControl.Core.Extensions;
using InventoryControl.Core.Results;
using InventoryControl.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryControl.Core.Services;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly AppDbContext _context;

    public InventoryService(ILogger<InventoryService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<MovementResult> AddMovementAsync(MovementDTO dto)
    {
        try
        {
            var product = await GetProductByCodeAsync(dto.ProductCode);

            if (product == null) return MovementResult.Fail(Constants.Messages.ProductNotFound);

            if (dto.Type == Constants.MovementTypes.Outbound)
            {
                var balance = await CalculateBalanceAsync(product.Id);

                if (dto.Quantity > balance)
                    return MovementResult.Fail(Constants.Messages.InsufficientStock);
            }

            var movement = dto.toModel(product.Id);
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

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
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(productCode))
                products = products.Where(p => p.Code == productCode);

            var stockReport = await products.Select(p => new StockReportDTO
            {
                ProductName = p.Name,
                ProductCode = p.Code,
                Inbound = p.Movements
                    .Where(m => m.Type == Constants.MovementTypes.Inbound && m.CreatedAt.Date == date.Date)
                    .Sum(m => m.Quantity),
                Outbound = p.Movements
                    .Where(m => m.Type == Constants.MovementTypes.Outbound && m.CreatedAt.Date == date.Date)
                    .Sum(m => m.Quantity)
            }).ToListAsync();

            return ReportResult.Sucess(stockReport);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return ReportResult.Fail(Constants.Messages.UnexpectedError);
        }
    }

    private async Task<Product?> GetProductByCodeAsync(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
    }

    private async Task<int> CalculateBalanceAsync(int productId, DateTime? until = null)
    {
        var query = _context.Movements.Where(m => m.ProductId == productId);
        if (until.HasValue)
            query = query.Where(m => m.CreatedAt.Date <= until.Value.Date);

        var inbound = await query.Where(m => m.Type == Constants.MovementTypes.Inbound).SumAsync(m => m.Quantity);
        var outbound = await query.Where(m => m.Type == Constants.MovementTypes.Outbound).SumAsync(m => m.Quantity);
        return inbound - outbound;
    }
}
