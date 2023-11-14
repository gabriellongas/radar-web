using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Radar.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApiClient _apiClient = new();
        internal static int CurrentUserID { get; set; } = -1;

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
                CurrentUserID = GetCurrentUserIdFromToken(token);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError("Unauthorized", "Usuário ou senha inválidos");
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
            CurrentUserID = -1;
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
