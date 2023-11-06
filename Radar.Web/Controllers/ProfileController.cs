using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using Radar.Web.Models.ViewModels;
using Radar.Web.Models;
using System.Diagnostics;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public IActionResult Index()
        {
            if (LoginController.CurrentUserID == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            ProfileViewModel profileViewModel = new (
                pessoa: _apiClient.GetPessoa(LoginController.CurrentUserID),
                posts: _apiClient.GetPostsFromPessoa(LoginController.CurrentUserID)
            );
            return View(profileViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
