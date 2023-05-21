using Diploma.model.user;
using Diploma.model.warehouse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.model.employee
{
    [Table(name: "Employees")]
    public class Employee : User
    {
        [Required, MaxLength(16), Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
        [Required] public Warehouse Warehouse { get; set; } = new();
        public override string Role => "EmployeeUser";
    }
}
