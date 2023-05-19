using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class DoctorRegistrationDTO
    {
        [Required] public string Login { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        [Required] public string Police { get; set; } = string.Empty;
        [Required] public string Offece { get; set; } = string.Empty;
    }
}
