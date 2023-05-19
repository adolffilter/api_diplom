using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class Medication
    {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; } = string.Empty;
    }
}
