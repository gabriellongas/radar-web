using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class LocaisController : Controller
    {
        private IApiClient _apiClient;
        private IConfiguration _configuration;
        private readonly List<LocalReadDto> _locais;

        public LocaisController(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
            _locais = _apiClient.GetLocal(HttpContext.Session.GetString("Token"));
        }

        public IActionResult Index(int id)
        {
            try
            {
                if (HttpContext.Session.GetInt32("UserID") == -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                ViewBag.CurrentUserId = HttpContext.Session.GetInt32("UserID");
                ViewBag.Url = $"{_configuration["ApiSettings:ApiURL"]}{ApiClient.CurtidaPath}";
                ViewBag.Token = HttpContext.Session.GetString("Token");

                LocalViewModel localViewModel = new();
                localViewModel.Locais = _locais.ToSelectListItem();

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

            if (_locais is null || !_locais.Any())
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }

            int selectedId = _locais.Single(local => local.Nome == selectedLocalName).LocalId;
            return RedirectToAction("Index", "Locais", new { id = selectedId });
        }
    }
}
