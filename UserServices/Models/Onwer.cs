using Microsoft.AspNetCore.Identity;

namespace UserServices.Models
{
    public class Onwer : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
