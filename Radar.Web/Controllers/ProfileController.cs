using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private IConfiguration _configuration;
        private IApiClient _apiClient;

        public ProfileController(IConfiguration configuration, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetInt32("UserID") == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id <= 0) id = (int)HttpContext.Session.GetInt32("UserID");

            ProfileViewModel profileViewModel = new()
            {
                Pessoa = _apiClient.GetPessoa(id, HttpContext.Session.GetString("Token")),
                Posts = _apiClient.GetPostsFromPessoa((int)HttpContext.Session.GetInt32("UserID"), id, HttpContext.Session.GetString("Token")).OrderByDescending(post => post.DataPostagem),
                IsMe = id == HttpContext.Session.GetInt32("UserID")
            };

            ViewBag.CurrentUserId = HttpContext.Session.GetInt32("UserID");
            ViewBag.Url = $"{_configuration["ApiSettings:ApiURL"]}{ApiClient.CurtidaPath}";
            ViewBag.Token = HttpContext.Session.GetString("Token");

            return View(profileViewModel);
        }

        public IActionResult Settings()
        {
            PessoaReadDto currentMe = _apiClient.GetPessoa((int)HttpContext.Session.GetInt32("UserID"), HttpContext.Session.GetString("Token"));
            Settings currentSettings = new()
            {
                Nome = currentMe.Nome,
                Email = currentMe.Email,
                Login = currentMe.Login,
                Descricao = currentMe.Descricao,
                DataNascimento = currentMe.DataNascimento,
                NewPassword = null,
                ConfirmPassword = null
            };

            SettingsViewModel viewModel = new()
            {
                Pessoa = currentMe,
                Settings = currentSettings
            };

            return View(viewModel);
        }

        public IActionResult Edit(SettingsViewModel newSettings)
        {
            PessoaReadDto currentMe = _apiClient.GetPessoa((int)HttpContext.Session.GetInt32("UserID"), HttpContext.Session.GetString("Token"));

            if (!ModelState.IsValid)
            {
                SettingsViewModel viewModel = new()
                {
                    Pessoa = currentMe,
                    Settings = newSettings.Settings
                };

                return View("Settings", viewModel);
            }

            PessoaLoginDto signIn = new()
            {
                Email = newSettings.Settings.Login,
                Login = newSettings.Settings.Login,
                Senha = newSettings.Settings.Senha
            };

            try
            {
                if (!_apiClient.ValidatePassword(signIn, HttpContext.Session.GetString("Token")))
                {
                    ModelState.AddModelError("Settings.Senha", "Senha incorreta");

                    SettingsViewModel viewModel = new()
                    {
                        Pessoa = currentMe,
                        Settings = newSettings.Settings
                    };

                    return View("Settings", viewModel);
                }

                PessoaUpdateDto newMe = new()
                {
                    PessoaId = (int)HttpContext.Session.GetInt32("UserID"),
                    Nome = newSettings.Settings.Nome,
                    Email = newSettings.Settings.Email,
                    Login = newSettings.Settings.Login,
                    Descricao = newSettings.Settings.Descricao,
                };

                _apiClient.PutPessoa(newMe, HttpContext.Session.GetString("Token"));

                if (!string.IsNullOrWhiteSpace(newSettings.Settings.NewPassword))
                {
                    UpdatePasswordDto newMePassword = new()
                    {
                        PessoaId = (int)HttpContext.Session.GetInt32("UserID"),
                        NewPassword = newSettings.Settings.NewPassword
                    };

                    _apiClient.UpdatePassword(newMePassword, HttpContext.Session.GetString("Token"));
                }

                return RedirectToAction("Index", "Profile", new { id = HttpContext.Session.GetInt32("UserID") });
            }
            catch (Exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }

        }
    }
}
