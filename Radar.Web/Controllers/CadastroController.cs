using Microsoft.AspNetCore.Mvc;

namespace Radar.Web.Controllers
{
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
