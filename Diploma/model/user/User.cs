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
    [Required] public string Police { get; set; } = string.Empty;
    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual List<Recipe> Recipes { get; set; } = new List<Recipe>();
    public virtual string Role => "BaseUser";
}