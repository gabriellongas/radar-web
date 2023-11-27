using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class CadastroController : Controller
    {
        private IApiClient _apiClient;
        public CadastroController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(PessoaCreateDto pessoa)
        {
            if (!ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                {
                    return View("Views/Cadastro/Index.cshtml", pessoa);
                }

                _apiClient.PostPessoa(pessoa, HttpContext.Session.GetString("Token"));

                return View("Views/Login/Index.cshtml");
            }

            if (!_apiClient.PostPessoa(pessoa))
            {
                return View("Views/Shared/Error.cshtml");
            }

            return View("Views/Login/Index.cshtml");
        }
    }
}
