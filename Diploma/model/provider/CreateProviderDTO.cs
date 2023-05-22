using System.ComponentModel.DataAnnotations;

namespace Diploma.model.provider
{
    public class CreateProviderDTO
    {
        [Required, MaxLength(16), Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string Address { get; set; } = string.Empty;
        [Required] public int UserId { get; set; }
    }
}
