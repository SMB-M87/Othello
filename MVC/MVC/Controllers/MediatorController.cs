using MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Authorize(Roles = "mediator")]
    public class MediatorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MediatorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Players()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7023/api/role/all");

            if (response.IsSuccessStatusCode)
            {
                var players = await response.Content.ReadFromJsonAsync<List<Player>>();
                return View(players);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve players.");
            return View(new List<Player>());
        }

        public async Task<IActionResult> ActiveGames()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7023/api/game/active");

            if (response.IsSuccessStatusCode)
            {
                var games = await response.Content.ReadFromJsonAsync<List<Game>>();
                return View(games);
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve active games.");
            return View(new List<Game>());
        }
    }
}
