using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.model.user
{
    [Table(name: "Patients")]
    public class Patient : User
    {
        public override string Role => "PatientUser";
    }
}
