using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class Recipe
    {
        [Key] public int Id { get; set; }
        [Required] public Doctor Doctor { get; set; } = new Doctor();
        [Required] public User Patient { get; set; } = new User();
        [Required] public Medication Medication { get; set; } = new Medication();
    }
}
