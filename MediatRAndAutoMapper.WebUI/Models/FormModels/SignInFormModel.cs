using System.ComponentModel.DataAnnotations;

namespace MediatRAndAutoMapper.WebUI.Models.FormModels
{
    public class SignInFormModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
