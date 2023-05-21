using System.ComponentModel.DataAnnotations;

namespace Diploma.model.user
{
    public class UpdateRecipeDto
    {
        [Required] public int PatientId { get; set; }
        [Required] public string MedicationText { get; set; } = string.Empty;
    }
}
