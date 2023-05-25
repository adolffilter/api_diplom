using System.ComponentModel.DataAnnotations;

namespace Diploma.model.provider
{
    public class UpdateProviderDTO
    {
        [Required, MaxLength(128)] public string Login { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string MidleName { get; set; } = string.Empty;
    }
}
