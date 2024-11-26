using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class GameController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public GameController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public IActionResult Play()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var api = _httpClient.BaseAddress + "api/";
                var token = _userManager.GetUserId(User);
                var redirect = "https://localhost:7269/";

                ViewBag.ApiUrl = api;
                ViewBag.Token = token;
                ViewBag.RedirectUrl = redirect;

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
