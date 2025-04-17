using InventoryControl.Core;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.API.Requests
{
    public class CreateMovementRequest
    {
        [Required]
        [MaxLength(Constants.MaxProductCodeLength)]
        public string ProductCode { get; set; } = null!;

        [Required]
        [RegularExpression("Inbound|Outbound")]
        public string Type { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
