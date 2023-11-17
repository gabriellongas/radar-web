using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using Radar.Web.Models.ViewModels;

namespace Radar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _apiClient = new ApiClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (LoginController.CurrentUserID == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            HomeViewModel homeViewModel = new()
            {
                Locais = _apiClient.GetLocal().ToSelectListItem(),
                Conteudo = "",
                Posts = _apiClient.GetPosts(LoginController.CurrentUserID).OrderByDescending(post => post.DataPostagem),
            };

            ViewBag.CurrentUserId = LoginController.CurrentUserID;
            ViewBag.Url = $"{ApiClient.Origin}{ApiClient.CurtidaPath}";
            ViewBag.Token = ApiClient.Token;

            return View(homeViewModel);
        }

        public IActionResult Publish(HomeViewModel post)
        {
            if (LoginController.CurrentUserID == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            post.Posts = _apiClient.GetPosts(LoginController.CurrentUserID);

            if (!ModelState.IsValid)
            {
                return View("Index", post);
            }

            PostCreateDto postCreateDto = new()
            {
                LocalId = (int)post.LocalId!,
                PessoaId = LoginController.CurrentUserID,
                Conteudo = post.Conteudo!,
                DataPostagem = DateTimeOffset.Now.DateTime,
                Avaliacao = new Random().Next(0, 6)
            };

            try { 

                _apiClient.PostPost(postCreateDto);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}