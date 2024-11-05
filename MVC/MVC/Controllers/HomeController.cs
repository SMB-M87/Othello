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
                var session = HttpContext.Session.GetString("GameCreation");
                var response = await GetView(token);

                var model = new Home
                {
                    Stats = response.Stats,
                    MatchHistory = response.MatchHistory,

                    Partial = new HomePartial
                    {
                        Pending = new HomePending()
                        {
                            Session = session ?? "false",
                            InGame = response.Partial.Pending.InGame,
                            Status = response.Partial.Pending.Status,
                            Games = response.Partial.Pending.Games
                        },

                        OnlinePlayers = response.Partial.OnlinePlayers,
                        PlayersInGame = response.Partial.PlayersInGame,
                        Friends = response.Partial.Friends,

                        FriendRequests = response.Partial.FriendRequests,
                        GameRequests = response.Partial.GameRequests,

                        SentFriendRequests = response.Partial.SentFriendRequests,
                        SentGameRequests = response.Partial.SentGameRequests,
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
                username = _userManager.GetUserName(User);
            }

            if (string.IsNullOrEmpty(username) || username == "Deleted")
            {
                return RedirectToAction("Index", "Home");
            }

            string currentUserId = _userManager.GetUserId(User);
            var response = await GetProfile($"{username} {currentUserId}");

            var model = new HomeProfile
            {
                Stats = response.Stats,
                Username = username,
                MatchHistory = response.MatchHistory,
                IsFriend = response.IsFriend,
                HasSentRequest = response.HasSentRequest,
                HasPendingRequest = response.HasPendingRequest,
                LastSeen = response.LastSeen
            };

            return View("Profile", model);
        }

        public async Task<IActionResult> Result(string token = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                var username = _userManager.GetUserName(User);
                var current_user_id = _userManager.GetUserId(User);

                var model = new GameOverview
                {
                    Username = username,
                    Result = await MostRecentGame(current_user_id) ?? new(),
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
            var response = await GetPartial(token);

            var model = new HomePartial
            {
                Pending = new HomePending()
                {
                    Session = session ?? "false",
                    InGame = response.Pending.InGame,
                    Status = response.Pending.Status,
                    Games = response.Pending.Games
                },

                OnlinePlayers = response.OnlinePlayers,
                PlayersInGame = response.PlayersInGame,
                Friends = response.Friends,

                FriendRequests = response.FriendRequests,
                GameRequests = response.GameRequests,

                SentFriendRequests = response.SentFriendRequests,
                SentGameRequests = response.SentGameRequests,
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

        private async Task<Home> GetView(string token)
        {
            var response = await _httpClient.GetAsync($"api/home/view/{token}");
            Home result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<Home>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }

        private async Task<HomePartial> GetPartial(string token)
        {
            var response = await _httpClient.GetAsync($"api/home/partial/{token}");
            HomePartial result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<HomePartial>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }

        private async Task<HomeProfile> GetProfile(string token)
        {
            var response = await _httpClient.GetAsync($"api/home/profile/{token}");
            HomeProfile result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<HomeProfile>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }

        private async Task<GameResult> MostRecentGame(string token)
        {
            var response = await _httpClient.GetAsync($"api/result/last/{token}");
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
