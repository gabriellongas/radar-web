using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models
{
    public class Pessoa
    {
        public int PessoaID { get; set; }

        [Required(ErrorMessage = "O nome deve ser preenchido")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
        [DataType(DataType.Text)]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail deve ser preenchido")]
        [MaxLength(50, ErrorMessage = "O e-mail deve ter no máximo 50 caracteres")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O nome de usuário deve ser preenchido")]
        [MaxLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "O nome de usuário deve ter no mínimo 3 caracteres")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "A senha deve ser preenchida")]
        [MaxLength(50, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = null!;

        [MaxLength(280, ErrorMessage = "A descrição deve ter no máximo 280 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de nascimento deve ser preenchida")]
        [DataType(DataType.Date, ErrorMessage = "A data deve ser válida")]
        public DateTimeOffset DataNascimento { get; set; }
    }
}
