using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user;

public class User
{
    [Key] public int Id { get; set; }
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string MidleName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string? Photo { get; set; } = null;
    public virtual string Role => "PatientUser";
}