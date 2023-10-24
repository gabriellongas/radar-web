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
    }
}