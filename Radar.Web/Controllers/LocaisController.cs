using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class LocaisController : Controller
    {
        private IApiClient _apiClient;
        private IConfiguration _configuration;

        public LocaisController(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public IActionResult Index(int id)
        {
            try
            {
                if (HttpContext.Session.GetInt32("UserID") == -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                List<LocalReadDto> locais = _apiClient.GetLocal(HttpContext.Session.GetString("Token"));

                ViewBag.CurrentUserId = HttpContext.Session.GetInt32("UserID");
                ViewBag.Url = $"{_configuration["ApiSettings:ApiURL"]}{ApiClient.CurtidaPath}";
                ViewBag.Token = HttpContext.Session.GetString("Token");

                LocalViewModel localViewModel = new();
                localViewModel.Locais = locais.ToSelectListItem();

                if (id <= 0)
                {
                    localViewModel.SelectedLocalName = null;
                    localViewModel.SelectedLocal = null;
                    localViewModel.Posts = new List<PostReadDto>();
                    return View(localViewModel);
                }
                
                localViewModel.SelectedLocal = _apiClient.GetLocal(id, HttpContext.Session.GetString("Token"));
                localViewModel.SelectedLocalName = localViewModel.SelectedLocal.Nome;
                localViewModel.Posts = _apiClient.GetPostsFromLocal((int)HttpContext.Session.GetInt32("UserID"), id, HttpContext.Session.GetString("Token")).OrderByDescending(post => post.DataPostagem);

                return View(localViewModel);

            }
            catch (Exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }
        }

        public IActionResult Select(string selectedLocalName)
        {
            if (selectedLocalName == null)
            {
                return RedirectToAction("Index", "Locais", new { id = -1 });
            }

            List<LocalReadDto> locais = _apiClient.GetLocal(HttpContext.Session.GetString("Token"));

            if (locais is null || !locais.Any())
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }

            int selectedId = locais.Single(local => local.Nome == selectedLocalName).LocalId;
            return RedirectToAction("Index", "Locais", new { id = selectedId });
        }
    }
}
