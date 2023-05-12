using System.ComponentModel.DataAnnotations;

namespace Diploma.model.specialization;

public class Specialization
{
    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public int Salary { get; set; }
}