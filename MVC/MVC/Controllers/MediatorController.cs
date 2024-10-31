using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize(Roles = "mediator")]
    public class MediatorController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public MediatorController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }


        public async Task<IActionResult> Players()
        {
            return View();
        }

        public async Task<IActionResult> Games()
        {
            return View();
        }

        public async Task<IActionResult> Results()
        {
            return View();
        }
    }
}
