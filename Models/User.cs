using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Model
{
    public class User : IdentityUser
    {
        public int Height { get; set; }
        public string Nickname { get; set; } = string.Empty;
    }
}
