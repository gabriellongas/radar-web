using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Radar.Web.Models.ViewModels
{
    public class SettingsViewModel
    {
        [ValidateNever]
        public PessoaReadDto Pessoa { get; set; } = null!;
        public Settings Settings { get; set; } = null!;
    }
}
