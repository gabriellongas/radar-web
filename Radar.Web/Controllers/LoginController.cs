using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Radar.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiClient _apiClient;

        public LoginController(IApiClient apiClient)
        {
                _apiClient = apiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn(SignIn signIn)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", signIn);
                }

                string token = _apiClient.SignIn(signIn);
                int id = GetCurrentUserIdFromToken(token);

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetInt32("UserID", id);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError("Error", "Senha inválida");
                return View("Index", signIn);
            }
            catch (HttpRequestException exception)
            {
                if (exception.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("Error", "Login não encontrado");
                    return View("Index", signIn);
                }

                ModelState.AddModelError("Error", "Ocorreu um erro inesperado");
                return View("Index", signIn);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Ocorreu um erro inesperado");
                return View("Index", signIn);
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.SetString("Token", "");
            HttpContext.Session.SetInt32("UserID", -1);

            return RedirectToAction("Index", "Login");
        }

        private static int GetCurrentUserIdFromToken(string token)
        {
            try
            {
                //Decode the token to build a ClaimsPrincipal object
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken? securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (securityToken == null) return -1;

                ClaimsIdentity claimsIdentity = new(securityToken.Claims, "Bearer");
                if (claimsIdentity == null) return -1;

                string? nameIdentifier = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (nameIdentifier == null) return -1;

                int currentUserID;
                if (!int.TryParse(nameIdentifier, out currentUserID)) return -1;

                return currentUserID;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
