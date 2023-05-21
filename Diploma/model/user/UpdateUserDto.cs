using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class UpdateUserDto
    {
        [Required] public string Login { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string MidleName { get; set; } = string.Empty;
    }
}
