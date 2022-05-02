using System.ComponentModel.DataAnnotations;

namespace IdentityServerApp.Dtos
{
    public class RegisterDtoModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;
    }
}
