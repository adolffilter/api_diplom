using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user;

public class AuthorizationDTO
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}