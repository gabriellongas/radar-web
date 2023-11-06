using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public int? LocalId { get; set; }
        public List<SelectListItem>? Locais { get; set; }

        [MaxLength(255, ErrorMessage = "O conteúdo não pode ter mais que 255 caracteres")]
        public string? Conteudo { get; set; }

        [Range(1, 5, ErrorMessage = "A avaliação deve ser um número entre 1 e 5")]
        public int? Avaliacao { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
