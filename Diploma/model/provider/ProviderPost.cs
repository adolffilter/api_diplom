using System.ComponentModel.DataAnnotations;

namespace Diploma.model.provider
{
    public class ProviderPost
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string Name { get; set; } = string.Empty;
    }
}
