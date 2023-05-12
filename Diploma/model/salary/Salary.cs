using System.ComponentModel.DataAnnotations;
using Diploma.model.user;

namespace Diploma.model.salary;

public class Salary
{
    [Key] public int Id { get; set; }
    [Required] public User User { get; set; }
    
}