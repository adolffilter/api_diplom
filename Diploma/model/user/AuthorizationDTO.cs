using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user;

public class AuthorizationDTO
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}