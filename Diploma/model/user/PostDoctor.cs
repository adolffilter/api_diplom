using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class PostDoctor
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Descriptin { get; set; } = string.Empty;
        [Required] public Doctor Author { get; set; } = new Doctor();
    }
}
