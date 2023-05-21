using System.ComponentModel.DataAnnotations;

namespace Diploma.model.warehouse
{
    public class Warehouse
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(1028)] public string Description { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
    }
}
