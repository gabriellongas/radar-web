using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

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

                LocalViewModel localViewModel = new();
                localViewModel.Locais = _locais.ToSelectListItem();

                if (id <= 0)
                {
                    localViewModel.SelectedLocalName = null;
                    localViewModel.SelectedLocal = null;
                    localViewModel.Posts = new();
                    return View(localViewModel);
                }
                
                localViewModel.SelectedLocal = _apiClient.GetLocal(id);
                localViewModel.SelectedLocalName = localViewModel.SelectedLocal.Nome;
                localViewModel.Posts = _apiClient.GetPostsFromLocal(LoginController.CurrentUserID, id);

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
