using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly List<LocalReadDto> _locais;

        public HomeController()
        {
            _apiClient = new ApiClient();
            _locais = _apiClient.GetLocal();
        }

        public IActionResult Index()
        {
            try
            {
                if (LoginController.CurrentUserID == -1)
                {
                    return RedirectToAction("Index", "Login");
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
            catch (Exception exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel() { Message = exception.Message });
            }
        }

        public IActionResult Publish(HomeViewModel post)
        {
            try
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

                LocalReadDto selectedLocal = _locais.Single(local => local.Nome == post.Review.SelectedLocalName);

                PostCreateDto postCreateDto = new()
                {
                    LocalId = selectedLocal.LocalId,
                    PessoaId = LoginController.CurrentUserID,
                    Conteudo = post.Review.Conteudo!,
                    DataPostagem = DateTimeOffset.Now.DateTime,
                    Avaliacao = post.Review.Avaliacao!.Value
                };

                _apiClient.PostPost(postCreateDto);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index", "Login");
            }
            catch (Exception exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel() { Message = exception.Message });
            }
        }
    }
}