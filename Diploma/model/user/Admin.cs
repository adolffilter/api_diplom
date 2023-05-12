using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.model.user;

[Table(name: "Admins")]
public class Admin : User
{
    public override string Role => "AdminUser";
}