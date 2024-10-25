using MVC.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7023/");
        }

        public async Task<IActionResult> Index()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var token = _userManager.GetUserId(User);
                var username = _userManager.GetUserName(User);

                List<string> online_players = await OnlinePlayers();
                List<string> friends = await Friends(token);
                List<string> online_friends = friends.Intersect(online_players).ToList();
                List<string> offline_friends = friends.Except(online_friends).ToList();

                List<GamePending> games = await PendingGames();
                List<string> joinable_players = games.Select(g => g.Username).ToList();

                var model = new HomeView
                {
                    Stats = await Stats(username),
                    MatchHistory = await MatchHistory(username),

                    OnlinePlayers = online_players,
                    OnlineFriends = online_friends,
                    OfflineFriends = offline_friends,

                    FriendRequests = await FriendRequests(token),
                    GameRequests = await GameRequests(token),

                    SentFriendRequests = await SentFriendRequests(token),
                    SentGameRequests = await SentGameRequests(token),
                    JoinablePlayers = joinable_players,

                    Games = games,
                };

                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            string stats = await Stats(username);
            List<GameResult> matchHistory = await MatchHistory(username);

            string currentUserId = _userManager.GetUserId(User);
            string currentUsername = _userManager.GetUserName(User);

            List<string> friends = await Friends(currentUserId);
            List<string> sentRequests = await SentFriendRequests(currentUsername);
            List<string> requests = await FriendRequests(currentUserId);

            var model = new ProfileView
            {
                Stats = stats,
                Username = username,
                MatchHistory = matchHistory,
                IsFriend = friends.Contains(username),
                HasSentRequest = sentRequests.Contains(username),
                HasPendingRequest = requests.Contains(username)
            };

            return View("Profile", model);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Wait(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            return View(model: token);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                ModelState.AddModelError(string.Empty, "Description cannot be empty.");
                return View("Create");
            }

            var userToken = _userManager.GetUserId(User);
            var createGameRequest = new
            {
                Player = userToken,
                Description = description
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/create", createGameRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to create game.");
                return View("Create");
            }

            var game = await _httpClient.GetAsync($"api/game/from/{userToken}");

            if (!game.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to find game.");
                return View("Create");
            }

            var token = await game.Content.ReadAsStringAsync();

            return RedirectToAction("Wait", new { token });
        }

        [HttpPost]
        public async Task<IActionResult> JoinGame(string username)
        {
            var request = new
            {
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/join", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to join game.");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Play", "Game");
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string username)
        {
            var request = new
            {
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend", request);

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
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend/accept", request);

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
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend/decline", request);

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
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/delete", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to decline friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendGameRequest(string username)
        {
            var request = new
            {
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to accept friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptGameRequest(string username)
        {
            var request = new
            {
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game/accept", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to accept friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineGameRequest(string username)
        {
            var request = new
            {
                ReceiverUsername = username,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game/decline", request);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to decline friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGameInvites()
        {
            var response = await _httpClient.PostAsJsonAsync("api/player/game/delete", _userManager.GetUserId(User));

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to decline friend request.");
            }

            var model = new HomeView
            {
                GameRequests = await GameRequests(_userManager.GetUserId(User))
            };

            return PartialView("_GameRequestsPartial", model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/game/delete/{token}", token);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game.");
                return RedirectToAction("Wait", new { player = token });
            }

            return RedirectToAction("Index");
        }

        private async Task<string> Stats(string username)
        {
            var statsResponse = await _httpClient.GetAsync($"api/result/stats/{username}");
            string stats = string.Empty;

            if (statsResponse.IsSuccessStatusCode)
            {
                stats = await statsResponse.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
            }
            return stats;
        }

        private async Task<List<GameResult>> MatchHistory(string username)
        {
            var historyResponse = await _httpClient.GetAsync($"api/result/history/{username}");
            List<GameResult> history = new();

            if (historyResponse.IsSuccessStatusCode)
            {
                history = await historyResponse.Content.ReadFromJsonAsync<List<GameResult>>() ?? new();
            }
            return history;
        }

        private async Task<List<string>> OnlinePlayers()
        {
            var response = await _httpClient.GetAsync("api/player");
            List<string> online = new();

            if (response.IsSuccessStatusCode)
            {
                online = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
                online.Remove(_userManager.GetUserName(User));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch online players.");
            }
            return online;
        }

        private async Task<List<string>> Friends(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/friends/{token}");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
            }
            return result;
        }

        private async Task<List<string>> FriendRequests(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/requests/friend/{token}");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
            }
            return result;
        }

        private async Task<List<string>> GameRequests(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/requests/game/{token}");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
            }
            return result;
        }

        private async Task<List<string>> SentFriendRequests(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/sent/friend/{token}");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
            }
            return result;
        }

        private async Task<List<string>> SentGameRequests(string token)
        {
            var response = await _httpClient.GetAsync($"api/player/sent/game/{token}");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
            }
            return result;
        }

        private async Task<List<GamePending>> PendingGames()
        {
            var response = await _httpClient.GetAsync("api/game");
            List<GamePending> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<GamePending>>() ?? new();
            }
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
