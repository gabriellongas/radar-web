using Microsoft.AspNetCore.Mvc;

namespace Radar.Web.Controllers
{
    public class LocaisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
