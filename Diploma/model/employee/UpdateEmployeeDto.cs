using System.ComponentModel.DataAnnotations;

namespace Diploma.model.employee
{
    public class UpdateEmployeeDto
    {
        [Required, MaxLength(16), Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
        [Required] public int WarehouseId { get; set; }
    }
}
