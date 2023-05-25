using Diploma.model.provider;
using System.ComponentModel.DataAnnotations;

namespace Diploma.model.order
{
    public class Order
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string Title { get; set; } = string.Empty;
        [Required, MaxLength(1080)] public string Description { get; set; } = string.Empty;
        [Required] public Provider Provider { get; set; } = new();

        public virtual bool Warehouse => false;
    }
}
