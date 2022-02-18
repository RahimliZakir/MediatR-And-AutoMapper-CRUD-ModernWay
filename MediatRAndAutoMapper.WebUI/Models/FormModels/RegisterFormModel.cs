using System.ComponentModel.DataAnnotations;

namespace MediatRAndAutoMapper.WebUI.Models.FormModels
{
    public class RegisterFormModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
