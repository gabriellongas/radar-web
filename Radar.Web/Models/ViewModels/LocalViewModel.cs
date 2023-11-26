using Microsoft.AspNetCore.Mvc.Rendering;

namespace Radar.Web.Models.ViewModels
{
    public class LocalViewModel
    {
        public List<SelectListItem> Locais { get; set; } = new List<SelectListItem>();
        public IEnumerable<PostReadDto> Posts { get; set; } = new List<PostReadDto>();
        public string? SelectedLocalName { get; set; }
        public LocalReadDto? SelectedLocal { get; set; }
    }
}
