﻿using Microsoft.AspNetCore.Mvc;
using Radar.Web.Api;
using Radar.Web.Models.ViewModels;
using Radar.Web.Models;
using System.Diagnostics;

namespace Radar.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public IActionResult Index()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel
            (
                pessoa: _apiClient.GetPessoa(1),
                posts: _apiClient.GetPostsFromPessoa(1)
            );
            return View(profileViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}