using System.ComponentModel.DataAnnotations;

namespace Diploma.model.employee
{
    public class UpdateEmployeeDto
    {
        [Required, MaxLength(16), Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
        [Required] public int WarehouseId { get; set; }
        [Required, MaxLength(128)] public string Login { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string MidleName { get; set; } = string.Empty;
    }
}
