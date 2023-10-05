using Microsoft.AspNetCore.Mvc;

namespace Radar.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
