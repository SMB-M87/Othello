using MVC.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            IHttpClientFactory httpClientFactory,
            UserManager<IdentityUser> userManager
            )
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async Task<IActionResult> Index()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                // Get the current user's ID
                var userId = _userManager.GetUserId(User);

                // Fetch online players
                var response = await _httpClient.GetAsync("api/player");
                List<string> onlinePlayers = new();

                if (response.IsSuccessStatusCode)
                {
                    onlinePlayers = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
                    onlinePlayers.Remove(_userManager.GetUserName(User));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to fetch online players.");
                }

                // Fetch friends, pending and sent requests
                var friendsResponse = await _httpClient.GetAsync($"api/player/friends/{userId}");
                List<string> friends = new();

                if (friendsResponse.IsSuccessStatusCode)
                {
                    friends = await friendsResponse.Content.ReadFromJsonAsync<List<string>>() ?? new();
                }

                var sentResponse = await _httpClient.GetAsync($"api/player/sent/{_userManager.GetUserName(User)}");
                List<string> sentRequests = new();

                if (sentResponse.IsSuccessStatusCode)
                {
                    sentRequests = await sentResponse.Content.ReadFromJsonAsync<List<string>>() ?? new();
                }

                var pendingResponse = await _httpClient.GetAsync($"api/player/pending/{userId}");
                List<string> pendingRequests = new();

                if (pendingResponse.IsSuccessStatusCode)
                {
                    pendingRequests = await pendingResponse.Content.ReadFromJsonAsync<List<string>>() ?? new();
                }

                // Create a ViewModel to pass data to the view
                var model = new HomeView
                {
                    OnlinePlayers = onlinePlayers,
                    Friends = friends,
                    SentRequests = sentRequests,
                    PendingRequests = pendingRequests
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string username)
        {
            var request = new
            {
                Receiver = username,
                Sender = _userManager.GetUserName(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/send", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to accept friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriendRequest(string username)
        {
            var request = new
            {
                Receiver = username,
                Sender = _userManager.GetUserName(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/accept", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to accept friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineFriendRequest(string username)
        {
            var request = new
            {
                Receiver = username,
                Sender = _userManager.GetUserName(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/decline", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to decline friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string username)
        {
            var request = new
            {
                Receiver = _userManager.GetUserName(User),
                Sender = username
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/delete", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to decline friend request.");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
