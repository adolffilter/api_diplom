using System.ComponentModel.DataAnnotations;

namespace Diploma.model.order
{
    public class CreateOrderDto
    {
        [Required, MaxLength(256)] public string Title { get; set; } = string.Empty;
        [Required, MaxLength(1080)] public string Description { get; set; } = string.Empty;
        [Required] public int ProviderId { get; set; }
    }
}
