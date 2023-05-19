using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user;

public class RegistrationDTO
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}