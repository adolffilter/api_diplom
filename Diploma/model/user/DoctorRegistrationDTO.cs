using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class DoctorRegistrationDTO
    {
        [Required] public int UserId { get; set; }
        [Required] public string Offece { get; set; } = string.Empty;
        [Required] public int PostId { get; set; }
    }
}
