using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models
{
    public class Settings : Pessoa
    {
        [MaxLength(50, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string? NewPassword { get; set; }

        [MaxLength(50, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem")]
        [Display(Name = "Confirmar Senha")]
        public string? ConfirmPassword { get; set; }
    }
}
