using System.ComponentModel.DataAnnotations;
using Diploma.model.specialization;

namespace Diploma.model.user;

public class User
{
    [Key] public int Id { get; set; }
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public int Balance { get; set; } = 0;
    [Required] public int Hourse { get; set; } = 0;
    [Required] public Specialization Specialization { get; set; }
    
    public virtual string Role => "BaseUser";
}