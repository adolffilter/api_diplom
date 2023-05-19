using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class PostDoctor
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
    }
}
