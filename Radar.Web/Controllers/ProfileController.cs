using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using Radar.Web.Models.ViewModels;
using Radar.Web.Models;
using System.Diagnostics;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiClient ApiClient = new ApiClient();

        public IActionResult Index()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel
            (
                pessoa: new Pessoa()
                {
                    PessoaID = 1,
                    Nome = "Thainá",
                    Email = "thaina@gmail.com",
                    Login = "Thainá",
                    Senha = "123456",
                    DataNascimento = DateTimeOffset.Now
                },
                posts: ApiClient.GetPosts()
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
