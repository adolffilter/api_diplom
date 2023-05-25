using Diploma.model.user;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.model.provider
{
    [Table(name: "Providers")]
    public class Provider : User
    {
        [Required] public ProviderPost Post { get; set; } = new();
        public override string Role => "ProviderUser";
    }
}
