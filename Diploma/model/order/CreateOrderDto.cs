using System.ComponentModel.DataAnnotations;

namespace Diploma.model.order
{
    public class CreateOrderDto
    {
        [Required] public string Addres { get; set; } = string.Empty;
        [Required] public int ProductId { get; set; } = new();
        [Required] public int Quantity { get; set; }
    }
}
