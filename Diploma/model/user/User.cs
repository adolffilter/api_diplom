using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user;

public class User
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(128)] public string Login { get; set; } = string.Empty;
    [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
    [Required, MaxLength(128)] public string MidleName { get; set; } = string.Empty;
    [Required, MaxLength(128)] public string Password { get; set; } = string.Empty;
    [MaxLength(512)]public string? Photo { get; set; } = null;
    public virtual string Role => "BaseUser";
}