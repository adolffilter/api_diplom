using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class DoctorRegistrationDTO
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string MidleName { get; set; } = string.Empty;
        [Required] public string Login { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        [Required] public string Police { get; set; } = string.Empty;
        [Required] public string Offece { get; set; } = string.Empty;
    }
}
