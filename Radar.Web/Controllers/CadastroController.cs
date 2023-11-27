using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using System.Net;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Views/Cadastro/Index.cshtml", pessoa);
                }

                _apiClient.PostPessoa(pessoa, HttpContext.Session.GetString("Token"));

                return View("Views/Login/Index.cshtml");
            }
            catch (HttpRequestException exception)
            {
                if (exception.StatusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError("Error", "Já existe um usuário com esse login ou e-mail");
                    return View("Index", pessoa);
                }

                ModelState.AddModelError("Error", "Ocorreu um erro inesperado");
                return View("Index", pessoa);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Error", "Ocorreu um erro inesperado");
                return View("Index", pessoa);
            }
        }
    }
}
