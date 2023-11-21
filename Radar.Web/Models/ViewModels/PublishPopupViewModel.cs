using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models.ViewModels
{
    public class PublishPopupViewModel
    {
        public List<SelectListItem> Locais { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "O local é obrigatório")]
        [Display(Name = "Local")]
        public string? SelectedLocalName { get; set; }

        [MaxLength(255, ErrorMessage = "O conteúdo não pode ter mais que 255 caracteres")]
        public string? Conteudo { get; set; }

        [Range(1, 5, ErrorMessage = "A avaliação deve ser um número entre 1 e 5")]
        public int? Avaliacao { get; set; }
    }
}
