using Microsoft.AspNetCore.Mvc.Rendering;
using Radar.Web.Models.ViewModels;

namespace Radar.Web.Models.Dto;

public static class DtoExtension
{
    #region Local
    public static List<SelectListItem> ToSelectListItem(this List<LocalReadDto> locais)
    {
        List<SelectListItem> localOptions = new();

        foreach (LocalReadDto local in locais)
        {
            SelectListItem localOption = new()
            {
                Value = $"{local.LocalId}",
                Text = local.Nome
            };

            localOptions.Add(localOption);
        }

        return localOptions;
    }
    #endregion Local

    #region Pessoa
    #endregion Pessoa

    #region Post
    #endregion Post

    #region SignIn
    public static PessoaLoginDto ToSignInDto(this SignIn signIn)
    {
        return new()
        {
            Email = signIn.Login,
            Login = signIn.Login,
            Senha = signIn.Senha
        };
    }
    #endregion SignIn
}