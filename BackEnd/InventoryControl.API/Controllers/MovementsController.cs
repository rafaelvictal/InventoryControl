using InventoryControl.API.Extensions;
using InventoryControl.API.Requests;
using InventoryControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementsController : ControllerBase
    {
        private readonly ILogger<MovementsController> _logger;
        private readonly IInventoryService _service;

        public MovementsController(ILogger<MovementsController> logger, IInventoryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovementRequest request)
        {
            _logger.LogInformation("Post-Start");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid Model.");
                return BadRequest(ModelState);
            }

            var dto = request.ToDTO();

            var result = await _service.AddMovementAsync(dto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("AddMovementAsync failed.");
                return BadRequest(result.Message);
            }

            var response = result.toResponse();

            _logger.LogInformation("Post-End-Sucess");

            return Ok(response);
        }

        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] DateTime date, [FromQuery] string? code)
        {
            _logger.LogInformation("Report-Start");

            if (date == default)
            {
                var msg = "Query parameter 'date' is required.";
                _logger.LogWarning(msg);
                return BadRequest(msg);
            }

            var result = await _service.GenerateReportAsync(date, code);

            if (!result.IsSucess)
            {
                _logger.LogWarning("Report failed.");
                return BadRequest(result.Message);
            }

            _logger.LogInformation("Report-End-Sucess");

            return Ok(result.StockReports);
        }
    }
}
