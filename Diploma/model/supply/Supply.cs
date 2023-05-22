using Diploma.model.product;
using Diploma.model.provider;
using Diploma.model.warehouse;
using System.ComponentModel.DataAnnotations;

namespace Diploma.model.supply
{
    public class Supply
    {
        [Key] public int Id { get; set; }
        [Required] public int Quantity { get; set; }
        public int Price
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
        [Required] public Product Product { get; set; } = new();
        [Required] public Provider Provider { get; set; } = new();
        [Required] public Warehouse Warehouse { get; set; } = new();
        [Required] public DateTime DateTime { get; set; } = DateTime.Now
    }
}
