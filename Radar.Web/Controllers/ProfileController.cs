using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public IActionResult Index(int id)
        {
            if (LoginController.CurrentUserID == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id <= 0) id = LoginController.CurrentUserID;

            ProfileViewModel profileViewModel = new()
            {
                Pessoa = _apiClient.GetPessoa(id),
                Posts = _apiClient.GetPostsFromPessoa(LoginController.CurrentUserID, id).OrderByDescending(post => post.DataPostagem),
                IsMe = id == LoginController.CurrentUserID
            };

            ViewBag.CurrentUserId = LoginController.CurrentUserID;
            ViewBag.Url = $"{ApiClient.Origin}{ApiClient.CurtidaPath}";
            ViewBag.Token = ApiClient.Token;

            return View(profileViewModel);
        }

        public IActionResult Settings()
        {
            PessoaReadDto currentMe = _apiClient.GetPessoa(LoginController.CurrentUserID);
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
            PessoaReadDto currentMe = _apiClient.GetPessoa(LoginController.CurrentUserID);

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
                if (!_apiClient.ValidatePassword(signIn))
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
                    PessoaId = LoginController.CurrentUserID,
                    Nome = newSettings.Settings.Nome,
                    Email = newSettings.Settings.Email,
                    Login = newSettings.Settings.Login,
                    Descricao = newSettings.Settings.Descricao,
                };

                _apiClient.PutPessoa(newMe);

                if (!string.IsNullOrWhiteSpace(newSettings.Settings.NewPassword))
                {
                    UpdatePasswordDto newMePassword = new()
                    {
                        PessoaId = LoginController.CurrentUserID,
                        NewPassword = newSettings.Settings.NewPassword
                    };

                    _apiClient.UpdatePassword(newMePassword);
                }

                return RedirectToAction("Index", "Profile", new { id = LoginController.CurrentUserID });
            }
            catch (Exception)
            {
                return View("Views/Shared/Error.cshtml", new ErrorViewModel());
            }
            
        }
    }
}
