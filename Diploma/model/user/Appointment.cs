using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class Appointment
    {
        [Key] public int Id { get; set; }
        [Required] public Doctor Doctor { get; set; } = new();
        [Required] public DateTime DateTime { get; set; }
        [Required] public User Pacient { get; set; } = new();
    }
}
