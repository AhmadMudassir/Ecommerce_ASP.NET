using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.Email
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
