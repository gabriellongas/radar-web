using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "O login deve ser preenchido")]
        [MaxLength(50, ErrorMessage = "O login deve ter no máximo 255 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha deve ser preenchida")]
        [MaxLength(50, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        [DataType(DataType.Password)]

        public string Senha { get; set; }
    }
}
