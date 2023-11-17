using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using System.Diagnostics;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public IActionResult Index(int id)
        {
            if (LoginController.CurrentUserID == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id <= 0) id = LoginController.CurrentUserID;

            ProfileViewModel profileViewModel = new (
                pessoa: _apiClient.GetPessoa(id),
                posts: _apiClient.GetPostsFromPessoa(LoginController.CurrentUserID, id)
            );

            ViewBag.CurrentUserId = LoginController.CurrentUserID;
            ViewBag.Url = $"{ApiClient.Origin}{ApiClient.CurtidaPath}";
            ViewBag.Token = ApiClient.Token;

            return View(profileViewModel);
        }
    }
}
