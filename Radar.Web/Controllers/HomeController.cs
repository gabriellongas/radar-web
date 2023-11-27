using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class HomeController : Controller
    {
        private IApiClient _apiClient;
        private IConfiguration _configuration;
        private readonly List<LocalReadDto> _locais;

        public HomeController(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _locais = _apiClient.GetLocal(HttpContext.Session.GetString("Token"));
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (LoginController.CurrentUserID == -1)
            {
                if (HttpContext.Session.GetInt32("UserID")== -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                HomeViewModel homeViewModel = new()
                {
                    Review = new PublishPopupViewModel()
                    {
                        Locais = _locais.ToSelectListItem()
                    },
                    Posts = _apiClient.GetPosts((int)HttpContext.Session.GetInt32("UserID"), HttpContext.Session.GetString("Token")).OrderByDescending(post => post.DataPostagem),
                };

                ViewBag.CurrentUserId = HttpContext.Session.GetInt32("UserID");
                ViewBag.Url = $"{_configuration["ApiSettings:ApiURL"]}{ApiClient.CurtidaPath}";
                ViewBag.Token = HttpContext.Session.GetString("Token");

                return View(homeViewModel);
            }

            HomeViewModel homeViewModel = new()
            {
                Review = new PublishPopupViewModel()
                {
                    Locais = _locais.ToSelectListItem()
                },
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
                if (HttpContext.Session.GetInt32("UserID") == -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                post.Posts = _apiClient.GetPosts((int)HttpContext.Session.GetInt32("UserID"), HttpContext.Session.GetString("Token"));

            if (!ModelState.IsValid)
            {
                return View("Index", post);
            }

            LocalReadDto selectedLocal = _locais.Single(local => local.Nome == post.Review.SelectedLocalName);

                PostCreateDto postCreateDto = new()
                {
                    LocalId = selectedLocal.LocalId,
                    PessoaId = (int)HttpContext.Session.GetInt32("UserID"),
                    Conteudo = post.Review.Conteudo!,
                    DataPostagem = DateTimeOffset.Now.DateTime,
                    Avaliacao = post.Review.Avaliacao!.Value
                };

                _apiClient.PostPost(postCreateDto, HttpContext.Session.GetString("Token"));

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