using Microsoft.AspNetCore.Identity;

namespace FirstApiProject.Models
{
    public class AppUser : IdentityUser
    {
       public string FullName { get; set; }
    }
}
