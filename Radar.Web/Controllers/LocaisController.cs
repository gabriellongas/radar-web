using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class LocaisController : Controller
    {
        private readonly ApiClient ApiClient = new();


        public IActionResult Index(int id)
        {
            try
            {
                if (LoginController.CurrentUserID == -1)
                {
                    return RedirectToAction("Index", "Login");
                }

                if (id <= 0)
                {
                    return View();
                }

                LocalReadDto local = ApiClient.GetLocal(id);

                return View(local);

            }
            catch (Exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }
        }
    }
}
