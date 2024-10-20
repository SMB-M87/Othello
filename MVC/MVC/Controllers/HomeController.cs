﻿using MVC.Models;
using System.Diagnostics;
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
                var userId = _userManager.GetUserId(User);

                // Fetch player stats
                var statsResponse = await _httpClient.GetAsync($"api/result/stats/{userId}");
                string stats = string.Empty;

                if (statsResponse.IsSuccessStatusCode)
                {
                    stats = await statsResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
                }

                // Fetch match history
                var historyResponse = await _httpClient.GetAsync($"api/result/history/{userId}");
                List<GameResult> matchHistory = new();

                if (historyResponse.IsSuccessStatusCode)
                {
                    matchHistory = await historyResponse.Content.ReadFromJsonAsync<List<GameResult>>() ?? new();
                }

                if (matchHistory.Count > 0)
                {
                    matchHistory = matchHistory.OrderByDescending(g => g.Date).ToList();

                    foreach (var game in matchHistory)
                    {
                        string url = "api/player/name/";

                        var winResponse = await _httpClient.GetAsync($"{url}{game.Winner}");
                        var loseResponse = await _httpClient.GetAsync($"{url}{game.Loser}");

                        game.Winner = await winResponse.Content.ReadAsStringAsync();
                        game.Loser = await loseResponse.Content.ReadAsStringAsync();
                    }
                }

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

                var onlineFriends = friends.Intersect(onlinePlayers).ToList();
                var offlineFriends = friends.Except(onlineFriends).ToList();
                friends = onlineFriends.Concat(offlineFriends).ToList();

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

                var gamesResponse = await _httpClient.GetAsync("api/game");
                List<GameDescription> games = new();

                if (gamesResponse.IsSuccessStatusCode)
                {
                    games = await gamesResponse.Content.ReadFromJsonAsync<List<GameDescription>>() ?? new();
                }

                var joinablePlayers = games.Select(g => g.Player).ToList();

                var model = new HomeView
                {
                    Stats = stats,
                    Friends = friends,
                    PendingGames = games,
                    Joinable = joinablePlayers,
                    SentRequests = sentRequests,
                    MatchHistory = matchHistory,
                    OnlinePlayers = onlinePlayers,
                    PendingRequests = pendingRequests
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> JoinGame(string username)
        {
            var playerTokenResponse = await _httpClient.GetAsync($"api/player/token/{username}");
            if (!playerTokenResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to find player.");
                return RedirectToAction("Index");
            }
            var playerToken = await playerTokenResponse.Content.ReadAsStringAsync();

            var entrant = new
            {
                Token = playerToken,
                Player = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/join/player", entrant);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to join game.");
                return RedirectToAction("Index");
            }

            var gameTokenRespons = await _httpClient.GetAsync($"api/game/from/{entrant.Player}");
            if (!gameTokenRespons.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to find game.");
                return RedirectToAction("Index");
            }
            var token = await gameTokenRespons.Content.ReadAsStringAsync();

            return RedirectToAction("Play", "Game", new { token });
        }

        [HttpPost]
        public async Task<IActionResult> JoinGameToken(string token)
        {
            var entrant = new
            {
                Token = token,
                Player = _userManager.GetUserId(User)
            };

            var response = await _httpClient.PostAsJsonAsync("api/game/join", entrant);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to join game.");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Play", "Game", new { token });
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

        public async Task<IActionResult> Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            // Fetch the token by username
            var respons = await _httpClient.GetAsync($"api/player/token/{username}");
            if (!respons.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            // Extract the token from the response
            var token = await respons.Content.ReadAsStringAsync();

            // Fetch player stats by token
            var statsResponse = await _httpClient.GetAsync($"api/result/stats/{token}");
            string stats = string.Empty;

            if (statsResponse.IsSuccessStatusCode)
            {
                stats = await statsResponse.Content.ReadAsStringAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to fetch player stats.");
            }

            // Fetch match history by token
            var historyResponse = await _httpClient.GetAsync($"api/result/history/{token}");
            List<GameResult> matchHistory = new();

            if (historyResponse.IsSuccessStatusCode)
            {
                matchHistory = await historyResponse.Content.ReadFromJsonAsync<List<GameResult>>() ?? new();
            }

            if (matchHistory.Count > 0)
            {
                matchHistory = matchHistory.OrderByDescending(g => g.Date).ToList();

                foreach (var game in matchHistory)
                {
                    string url = "api/player/name/";

                    var winResponse = await _httpClient.GetAsync($"{url}{game.Winner}");
                    var loseResponse = await _httpClient.GetAsync($"{url}{game.Loser}");

                    game.Winner = await winResponse.Content.ReadAsStringAsync();
                    game.Loser = await loseResponse.Content.ReadAsStringAsync();
                }
            }

            // Fetch the logged-in user's ID and friends
            var currentUserId = _userManager.GetUserId(User);
            var friendsResponse = await _httpClient.GetAsync($"api/player/friends/{currentUserId}");
            var friends = friendsResponse.IsSuccessStatusCode ? await friendsResponse.Content.ReadFromJsonAsync<List<string>>() ?? new() : new();

            // Fetch sent and pending friend requests
            var sentRequestsResponse = await _httpClient.GetAsync($"api/player/sent/{_userManager.GetUserName(User)}");
            var sentRequests = sentRequestsResponse.IsSuccessStatusCode ? await sentRequestsResponse.Content.ReadFromJsonAsync<List<string>>() ?? new() : new();

            var pendingRequestsResponse = await _httpClient.GetAsync($"api/player/pending/{currentUserId}");
            var pendingRequests = pendingRequestsResponse.IsSuccessStatusCode ? await pendingRequestsResponse.Content.ReadFromJsonAsync<List<string>>() ?? new() : new();

            var model = new ProfileView
            {
                Stats = stats,
                Username = username,
                MatchHistory = matchHistory,
                IsFriend = friends.Contains(username),
                HasSentRequest = sentRequests.Contains(username),
                HasPendingRequest = pendingRequests.Contains(username)
            };

            return View("Profile", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
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

            return RedirectToAction("Wait", "Game", new { token });
        }

        public async Task<IActionResult> Result(string token)
        {
            var resultResponse = await _httpClient.GetAsync($"api/result/{token}");

            if (resultResponse.IsSuccessStatusCode)
            {
                GameResult result = await resultResponse.Content.ReadFromJsonAsync<GameResult>() ?? new();

                string url = "api/player/name/";

                var winResponse = await _httpClient.GetAsync($"{url}{result.Winner}");
                var loseResponse = await _httpClient.GetAsync($"{url}{result.Loser}");

                result.Winner = await winResponse.Content.ReadAsStringAsync();
                result.Loser = await loseResponse.Content.ReadAsStringAsync();

                return View("Result", result);
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
