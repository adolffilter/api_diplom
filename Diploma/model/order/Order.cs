using Diploma.model.product;
using Diploma.model.user;
using System.ComponentModel.DataAnnotations;

namespace Diploma.model.order
{
    public class Order
    {
        [Key] public int Id { get; set; }
        [Required] public string Addres { get; set; } = string.Empty;
        public int Price
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
        [Required] public Product Product { get; set; } = new();
        [Required] public int Quantity { get; set; }
        [Required] public DateTime DateTime { get; set; } = DateTime.Now
        [Required] public User User { get; set; } = new();
    }
}
