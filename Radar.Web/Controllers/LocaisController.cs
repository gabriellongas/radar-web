using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using System.Net;

namespace Radar.Web.Controllers
{
    public class LocaisController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly List<LocalReadDto> _locais;

        public LocaisController()
        {
            _apiClient = new ApiClient();
            _locais = _apiClient.GetLocal();
        }

        public IActionResult Index(int id)
        {
            try
            {
                if (LoginController.CurrentUserID == -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                ViewBag.CurrentUserId = LoginController.CurrentUserID;
                ViewBag.Url = $"{ApiClient.Origin}{ApiClient.CurtidaPath}";
                ViewBag.Token = ApiClient.Token;

                LocalViewModel localViewModel = new();
                localViewModel.Locais = _locais.ToSelectListItem();

                if (id <= 0)
                {
                    localViewModel.SelectedLocalName = null;
                    localViewModel.SelectedLocal = null;
                    localViewModel.Posts = new List<PostReadDto>();
                    return View(localViewModel);
                }
                
                localViewModel.SelectedLocal = _apiClient.GetLocal(id);
                localViewModel.SelectedLocalName = localViewModel.SelectedLocal.Nome;
                localViewModel.Posts = _apiClient.GetPostsFromLocal(LoginController.CurrentUserID, id).OrderByDescending(post => post.DataPostagem);

                return View(localViewModel);

            }
            catch (HttpRequestException exception)
            {
                if (exception.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Login");
                }

                if (exception.StatusCode == HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("Error", "Local não encontrado");
                    return RedirectToAction("Index", "Locais", new { id = -1 });
                }

                ModelState.AddModelError("Error", exception.Message);
                return RedirectToAction("Index", "Locais", new { id = -1 });
            }
            catch (Exception exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel() { Message = exception.Message});
            }
        }

        public IActionResult Select(string selectedLocalName)
        {
            try
            {
                if (selectedLocalName == null)
                {
                    return RedirectToAction("Index", "Locais", new { id = -1 });
                }

                if (_locais is null || !_locais.Any())
                {
                    return View("Views/Shared/Error.cshtml", new ErrorViewModel());
                }

                if (!_locais.Any(local => local.Nome == selectedLocalName))
                {
                    ModelState.AddModelError("Error", "Local não encontrado");
                    LocalViewModel localViewModel = new();
                    localViewModel.Locais = _locais.ToSelectListItem();

                    ViewBag.CurrentUserId = LoginController.CurrentUserID;
                    ViewBag.Url = $"{ApiClient.Origin}{ApiClient.CurtidaPath}";
                    ViewBag.Token = ApiClient.Token;

                    return View("Index", localViewModel);
                }

                int selectedId = _locais.Single(local => local.Nome == selectedLocalName).LocalId;
                return RedirectToAction("Index", "Locais", new { id = selectedId });
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("Error", exception.Message);
                return RedirectToAction("Index", "Locais", new { id = -1 });
            }
        }
    }
}
