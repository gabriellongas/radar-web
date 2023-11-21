using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Radar.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public PublishPopupViewModel Review { get; set; } = new PublishPopupViewModel();

        public IEnumerable<PostReadDto>? Posts { get; set; }
    }
}
