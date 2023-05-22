using System.ComponentModel.DataAnnotations;

namespace Diploma.model.supply
{
    public class CreateSupplyDto
    {
        [Required] public int Quantity { get; set; }
        [Required] public int ProductId { get; set; } = new();
        [Required] public int ProviderId { get; set; } = new();
        [Required] public int WarehouseId { get; set; } = new();
    }
}
