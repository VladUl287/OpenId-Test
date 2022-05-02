using System.ComponentModel.DataAnnotations;

namespace IdentityServerApp.Dtos
{
    public class LoginDtoModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
    }
}