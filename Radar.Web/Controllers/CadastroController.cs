using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ApiClient _apiClient = new();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return View("Views/Cadastro/Index.cshtml", pessoa);
            }

            if (!_apiClient.PostPessoa(pessoa))
            {
                return View("Views/Shared/Error.cshtml");
            }

            return View("Views/Login/Index.cshtml");
        }
    }
}
