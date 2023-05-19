using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class UpdateDoctorDTO
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string MidleName { get; set; } = string.Empty;
        [Required] public string Offece { get; set; } = string.Empty;
        [Required] public int PostId { get; set; } 
    }
}
