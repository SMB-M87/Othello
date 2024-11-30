using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public HomeController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new Exception("BaseUrl setting is missing in configuration.");

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            var cookies = _httpContextAccessor?.HttpContext?.Request.Cookies;

            if (cookies is not null)
            {
                foreach (var cookie in cookies)
                {
                    if (cookie.Key == ".AspNet.SharedAuthCookie")
                    {
                        handler.CookieContainer.Add(
                            new Uri(baseUrl),
                            new Cookie(cookie.Key, cookie.Value)
                        );
                    }
                }

                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseUrl)
                };
            }
            else
            {
                _httpClient = httpClientFactory.CreateClient("ApiClient");
            }
        }

        public async Task<IActionResult> Index()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var token = _userManager.GetUserId(User);
                var session = HttpContext.Session.GetString("GameCreation");
                var response = await GetView(token);

                var model = new User
                {
                    Stats = response.Stats,
                    MatchHistory = response.MatchHistory,

                    Partial = new UserPartial
                    {
                        Pending = new UserPending()
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

        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Profile(string username = "")
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
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
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Result(string token)
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated && !string.IsNullOrEmpty(token))
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

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creation()
        {
            HttpContext.Session.SetString("GameCreation", "true");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel()
        {
            HttpContext.Session.SetString("GameCreation", "false");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        public async Task<IActionResult> Partial()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var token = _userManager.GetUserId(User);
                var session = HttpContext.Session.GetString("GameCreation");
                var response = await GetPartial(token);

                var model = new UserPartial
                {
                    Pending = new UserPending()
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
                    SentGameRequests = response.SentGameRequests
                };
                return PartialView("_Partial", model);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        [Authorize(Roles = Roles.User)]
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

        private async Task<User> GetView(string token)
        {
            var response = await _httpClient.GetAsync($"api/user/view/{token}");
            User result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<User>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }

        private async Task<UserPartial> GetPartial(string token)
        {
            var response = await _httpClient.GetAsync($"api/user/partial/{token}");
            UserPartial result = new();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new ColorArrayConverter() }
                };

                var deserializedResult = await response.Content.ReadFromJsonAsync<UserPartial>(options);

                if (deserializedResult is not null)
                {
                    result = deserializedResult;
                }
            }
            return result;
        }

        private async Task<HomeProfile> GetProfile(string token)
        {
            var response = await _httpClient.GetAsync($"api/user/profile/{token}");
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
    }
}
