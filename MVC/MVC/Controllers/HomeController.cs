using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IConfiguration configuration,
                              IHttpClientFactory httpClientFactory,
                              UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl ?? throw new ArgumentNullException(nameof(configuration), "BaseUrl setting is missing in configuration."));
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };
        }

        public async Task<IActionResult> Index()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var token = _userManager.GetUserId(User);
                var username = _userManager.GetUserName(User);

                DeleteGameInvites(token);
                var session = HttpContext.Session.GetString("GameCreation");

                var model = new Home
                {
                    Stats = await Stats(username),
                    MatchHistory = await MatchHistory(username),

                    Partial = new HomePartial
                    {
                        Pending = new HomePending()
                        {
                            Session = session ?? "false",
                            InGame = await InGame(token),
                            Status = await Status(token),
                            Games = await PendingGames()
                        },

                        OnlinePlayers = await OnlinePlayers(),
                        PlayersInGame = await PlayersInGame(),
                        Friends = await Friends(token),

                        FriendRequests = await FriendRequests(token),
                        GameRequests = await GameRequests(token),

                        SentFriendRequests = await SentFriendRequests(token),
                        SentGameRequests = await SentGameRequests(token)
                    }
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

            var model = new HomeProfile
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

        public async Task<IActionResult> Result()
        {
            var username = _userManager.GetUserName(User);

            var model = new GameOverview
            {
                Username = username,
                Result = await MostRecentGame(username) ?? new()
            };

            return View("Result", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creation()
        {
            HttpContext.Session.SetString("GameCreation", "true");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel()
        {
            HttpContext.Session.SetString("GameCreation", "false");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string description)
        {
            var token = _userManager.GetUserId(User);
            var createGameRequest = new
            {
                PlayerToken = token,
                Description = description
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/create", createGameRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to create game.");
                return RedirectToAction("Index");
            }

            var game = await _httpClient.GetAsync($"api/game/{token}");

            if (!game.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to find game.");
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("GameCreation", "false");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Partial()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/player/game/delete", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game requests.");
            }

            DeleteGameInvites(token);
            var session = HttpContext.Session.GetString("GameCreation");

            var model = new HomePartial
            {
                Pending = new HomePending()
                {
                    Session = session ?? "false",
                    InGame = await InGame(token),
                    Status = await Status(token),
                    Games = await PendingGames()
                },

                OnlinePlayers = await OnlinePlayers(),
                PlayersInGame = await PlayersInGame(),
                Friends = await Friends(token),

                FriendRequests = await FriendRequests(token),
                GameRequests = await GameRequests(token),

                SentFriendRequests = await SentFriendRequests(token),
                SentGameRequests = await SentGameRequests(token)
            };
            return PartialView("_Partial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                ModelState.AddModelError(string.Empty, "Unable to send friend request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
                ModelState.AddModelError(string.Empty, "Unable to delete friend.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                ModelState.AddModelError(string.Empty, "Unable to send game request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
                ModelState.AddModelError(string.Empty, "Unable to decline game request.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/game/delete", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game.");
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("GameCreation", "false");
            return RedirectToAction("Index");
        }

        private async Task<string> Stats(string username)
        {
            var response = await _httpClient.GetAsync($"api/result/stats/{username}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
            }
            return result;
        }

        private async Task<List<GameResult>> MatchHistory(string username)
        {
            var response = await _httpClient.GetAsync($"api/result/history/{username}");
            List<GameResult> result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<List<GameResult>>(options) ?? new();
            }
            return result;
        }

        private async Task<bool> InGame(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/{token}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result != string.Empty;
        }

        private async Task<string> Status(string token)
        {
            var response = await _httpClient.GetAsync($"api/game/status/{token}");
            var result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        private async Task<List<string>> OnlinePlayers()
        {
            var response = await _httpClient.GetAsync("api/player");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
                result.Remove(_userManager.GetUserName(User));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch online players.");
            }
            return result;
        }

        private async Task<List<string>> PlayersInGame()
        {
            var response = await _httpClient.GetAsync("api/player/gaming");
            List<string> result = new();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
                result.Remove(_userManager.GetUserName(User));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch online players.");
            }
            return result;
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

        private async void DeleteGameInvites(string token)
        {
            var response = await _httpClient.PostAsJsonAsync("api/player/game/delete", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete game requests.");
            }
        }

        private async Task<GameResult?> MostRecentGame(string username)
        {
            var response = await _httpClient.GetAsync($"api/result/history/{username}");
            List<GameResult> result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<List<GameResult>>(options) ?? new();
            }

            var closestGame = result.OrderBy(game => Math.Abs((DateTime.UtcNow - game.Date).Ticks)).FirstOrDefault();
            return closestGame;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
