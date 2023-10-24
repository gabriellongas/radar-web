using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Radar.Web.Models;
using Radar.Web.Models.ViewModels;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient ApiClient = new ApiClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
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