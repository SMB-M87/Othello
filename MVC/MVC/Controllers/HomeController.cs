using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
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

        public async Task<IActionResult> Profile(string username = "")
        {
            if (string.IsNullOrEmpty(username))
            {
                username = User != null && User.Identity != null && User.Identity.Name != null ? User.Identity.Name : string.Empty;
            }

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            string currentUserId = _userManager.GetUserId(User);

            var model = new HomeProfile
            {
                Stats = await Stats(username),
                Username = username,
                MatchHistory = await MatchHistory(username),
                IsFriend = await IsFriend(username, currentUserId),
                HasSentRequest = await HasSentRequest(username, currentUserId),
                HasPendingRequest = await HasPendingRequest(username, currentUserId),
                LastSeen = await LastSeen(username)
            };

            return View("Profile", model);
        }

        public async Task<IActionResult> Result(string token = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                var username = _userManager.GetUserName(User);

                var model = new GameOverview
                {
                    Username = username,
                    Result = await MostRecentGame(username) ?? new(),
                    Rematch = true,
                    Request = string.Empty
                };

                if (model.Result != null && model.Result.Board != null)
                {
                    return View("Result", model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string[] parts = token.Split(' ');

                var model = new GameOverview
                {
                    Username = parts[0],
                    Result = await GetResult(parts[1]) ?? new(),
                    Rematch = false
                };
                return View("Result", model);
            }
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
        public async Task<IActionResult> Partial()
        {
            var token = _userManager.GetUserId(User);
            var session = HttpContext.Session.GetString("GameCreation");
            DeleteGameInvites(token);

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
        public async Task<IActionResult> Rematch([FromBody] Text text)
        {
            var token = _userManager.GetUserId(User);

            var model = new Text
            {
                Body = await Rematch(text.Body, token)
            };
            return PartialView("_Rematch", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateGame([FromBody] Text text)
        {
            var token = _userManager.GetUserId(User);

            var createGameRequest = new
            {
                PlayerToken = token,
                Description = text.Body
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/create", createGameRequest);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to create game." });
            }

            var game = await _httpClient.GetAsync($"api/game/{token}");

            if (!game.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Game creation failed." });
            }
            HttpContext.Session.SetString("GameCreation", "false");
            return Json(new { success = true, message = "Game created." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RematchGame([FromBody] Text text)
        {
            var token = _userManager.GetUserId(User);

            var createGameRequest = new
            {
                PlayerToken = token,
                Description = $"Rematch against {text.Body}",
                Rematch = text.Body
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/create", createGameRequest);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to create game." });
            }

            var game = await _httpClient.GetAsync($"api/game/{token}");

            if (!game.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Game creation failed." });
            }
            HttpContext.Session.SetString("GameCreation", "false");
            return Json(new { success = true, message = "Game created." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteGame()
        {
            var token = _userManager.GetUserId(User);
            var response = await _httpClient.PostAsJsonAsync("api/game/delete", new { Token = token });

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to delete game." });
            }
            HttpContext.Session.SetString("GameCreation", "false");
            return Json(new { success = true, message = "Game deleted." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> JoinGame([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/join", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to join game." });
            }
            return Json(new { success = true, message = "Game joined." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SendFriendRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to send friend request." });
            }
            return Json(new { success = true, message = "Friend request sent." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AcceptFriendRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend/accept", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to accept friend request." });
            }
            return Json(new { success = true, message = "Friend request accepted." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeclineFriendRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/friend/decline", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to decline friend request." });
            }
            return Json(new { success = true, message = "Friend request declined." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteFriend([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/friend/delete", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to delete friend." });
            }
            return Json(new { success = true, message = "Friend deleted." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SendGameRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to send game request." });
            }
            return Json(new { success = true, message = "Game request sent." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AcceptGameRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game/accept", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to accept game request." });
            }
            return Json(new { success = true, message = "Game request accepted." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeclineGameRequest([FromBody] Text player)
        {
            var request = new
            {
                ReceiverUsername = player.Body,
                SenderToken = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/player/request/game/decline", request);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Unable to decline game request." });
            }
            return Json(new { success = true, message = "Game request declined." });
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

            var lastGame = result.OrderBy(game => Math.Abs((DateTime.UtcNow - game.Date).Ticks)).FirstOrDefault();
            return lastGame;
        }

        private async Task<GameResult?> GetResult(string token)
        {
            var response = await _httpClient.GetAsync($"api/result/{token}");
            GameResult result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                result = await response.Content.ReadFromJsonAsync<GameResult>(options) ?? new();
            }
            return result;
        }

        private async Task<bool> IsFriend(string receiver_username, string sender_token)
        {
            var request = receiver_username + " " + sender_token;

            var response = await _httpClient.GetAsync($"api/player/is/friend/{request}");
            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }
            return result;
        }

        private async Task<bool> HasSentRequest(string receiver_username, string sender_token)
        {
            var request = receiver_username + " " + sender_token;

            var response = await _httpClient.GetAsync($"api/player/has/sent/friend/{request}");
            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }
            return result;
        }

        private async Task<bool> HasPendingRequest(string receiver_username, string sender_token)
        {
            var request = receiver_username + " " + sender_token;

            var response = await _httpClient.GetAsync($"api/player/has/received/friend/{request}");
            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }
            return result;
        }

        private async Task<string> LastSeen(string username)
        {
            var response = await _httpClient.GetAsync($"api/player/last/seen/{username}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync() ?? string.Empty;
            }
            return result;
        }

        private async Task<string> Rematch(string receiver_username, string sender_token)
        {
            var request = receiver_username + " " + sender_token;

            var response = await _httpClient.GetAsync($"api/player/rematch/{request}");
            string result = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch rematch data.");
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
