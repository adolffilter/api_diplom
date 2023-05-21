using System.ComponentModel.DataAnnotations;

namespace Diploma.model.provider
{
    public class Provider
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string MidleName { get; set; } = string.Empty;
        [Required, MaxLength(16), Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
    }
}
