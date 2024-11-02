using API.Models;

namespace API.Data
{
    public class HomeAccessLayer : IHomeRepository
    {
        private readonly Database _context;

        public HomeAccessLayer(Database context)
        {
            _context = context;
        }

        public Home GetView(string token)
        {
            var game = GetPlayersGame(token);
            var username = GetPlayersName(token);

            var model = new Home
            {
                Stats = string.IsNullOrEmpty(username) ? string.Empty : GetPlayerStats(username),
                MatchHistory = string.IsNullOrEmpty(username) ? new() : GetPlayersMatchHistory(username),
                Partial = new HomePartial()
                {
                    Pending = new HomePending()
                    {
                        Session = "false",
                        InGame = game != null,
                        Status = game != null ? ((int)game.Status).ToString() : string.Empty,
                        Games = GetPendingGames() ?? new()
                    },

                    OnlinePlayers = GetOnlinePlayers(token) ?? new(),
                    PlayersInGame = GetPlayersInGame(token) ?? new(),
                    Friends = GetFriends(token) ?? new(),

                    FriendRequests = GetFriendRequests(token) ?? new(),
                    GameRequests = GetGameRequests(token) ?? new(),

                    SentFriendRequests = GetSentFriendRequests(token) ?? new(),
                    SentGameRequests = GetSentGameRequests(token) ?? new()
                }
            };

            return model;
        }

        public HomePartial GetPartial(string token)
        {
            var game = GetPlayersGame(token);

            var model = new HomePartial
            {
                Pending = new HomePending()
                {
                    Session = "false",
                    InGame = game != null,
                    Status = game != null ? ((int)game.Status).ToString() : string.Empty,
                    Games = GetPendingGames() ?? new()
                },

                OnlinePlayers = GetOnlinePlayers(token) ?? new(),
                PlayersInGame = GetPlayersInGame(token) ?? new(),
                Friends = GetFriends(token) ?? new(),

                FriendRequests = GetFriendRequests(token) ?? new(),
                GameRequests = GetGameRequests(token) ?? new(),

                SentFriendRequests = GetSentFriendRequests(token) ?? new(),
                SentGameRequests = GetSentGameRequests(token) ?? new()
            };

            return model;
        }

        public HomeProfile GetProfile(string username, string token)
        {
            HomeProfile profile = new()
            {
                Stats = GetPlayerStats(username) ?? string.Empty,
                Username = username ?? string.Empty,
                MatchHistory = username != null ? GetPlayersMatchHistory(username) : new(),
                IsFriend = username != null && IsFriend(username, token),
                HasSentRequest = username != null && HasSentFriendRequest(username, token),
                HasPendingRequest = username != null && HasFriendRequest(username, token),
                LastSeen = GetLastActivity(username ?? string.Empty)
            };

            return profile;
        }

        private string? GetPlayersName(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token == token)?.Username;
        }

        private string? GetPlayersToken(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username == username)?.Token;
        }

        public Game? GetPlayersGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First == token && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second == token && s.Status != Status.Finished);

            return game;
        }

        public string GetPlayerStats(string username)
        {
            var token = GetPlayersToken(username);

            if (token != null)
            {
                List<GameResult> results = GetMatchHistory(token);

                int wins = results.Count(r => r.Winner == token && r.Draw == false);
                int losses = results.Count(r => r.Loser == token && r.Draw == false);
                int draws = results.Count(r => (r.Winner == token || r.Loser == token) && r.Draw == true);

                return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
            }
            return string.Empty;
        }

        private List<GameResult> GetMatchHistory(string token)
        {
            return _context.Results
                .Where(s => s.Winner == token || s.Loser == token)
                .ToList();
        }

        private List<GameResult> GetPlayersMatchHistory(string username)
        {
            var token = GetPlayersToken(username);

            if (token is not null)
            {
                var response = GetMatchHistory(token);

                if (response.Count > 0)
                {
                    response = response.OrderByDescending(r => r.Date).ToList();

                    foreach (var game in response)
                    {
                        game.Winner = GetPlayersName(game.Winner) ?? string.Empty;
                        game.Loser = GetPlayersName(game.Loser) ?? string.Empty;
                    }
                }
                return response;
            }
            return new();
        }

        private List<GamePending>? GetPendingGames()
        {
            var games = _context.Games.ToList();

            if (games is not null)
            {
                var pending = games.FindAll(game => game.Status == Status.Pending && game.Second == null && game.Rematch == null).OrderByDescending(g => g.Date).ToList();

                if (pending is not null)
                {
                    List<GamePending> result = new();

                    foreach (Game game in pending)
                    {
                        var player = GetPlayersName(game.First) ?? string.Empty;
                        var stat = GetPlayerStats(player) ?? string.Empty;
                        string output = stat.Replace("Wins:", "W:")
                                            .Replace("Losses:", "L:")
                                            .Replace("Draws:", "D:")
                                            .Replace("\t\t", " ");

                        GamePending temp = new(game.Description, player, output);
                        result.Add(temp);
                    }
                    return result;
                }
            }
            return null;
        }

        private string GetLastActivity(string username)
        {
            DateTime? lastActivity = _context.Players.FirstOrDefault(p => p.Username == username)?.LastActivity;

            if (lastActivity == null || lastActivity == DateTime.MinValue)
            {
                return "No recent activity";
            }

            TimeSpan timeSinceLastActivity = DateTime.UtcNow - lastActivity.Value;

            if (timeSinceLastActivity.TotalMinutes < 1)
                return "Just now";
            else if (timeSinceLastActivity.TotalMinutes < 60)
                return $"{Math.Floor(timeSinceLastActivity.TotalMinutes)} minutes ago";
            else if (timeSinceLastActivity.TotalHours < 24)
                return $"{Math.Floor(timeSinceLastActivity.TotalHours)} hours ago";
            else if (timeSinceLastActivity.TotalDays < 7)
                return $"{Math.Floor(timeSinceLastActivity.TotalDays)} days ago";
            else if (timeSinceLastActivity.TotalDays < 30)
                return $"{Math.Floor(timeSinceLastActivity.TotalDays / 7)} weeks ago";
            else if (timeSinceLastActivity.TotalDays < 365)
                return $"{Math.Floor(timeSinceLastActivity.TotalDays / 30)} months ago";
            else
                return $"{Math.Floor(timeSinceLastActivity.TotalDays / 365)} years ago";
        }

        private List<string>? GetOnlinePlayers(string token)
        {
            return _context.Players.ToList().FindAll(player => player.Token != token && (DateTime.UtcNow - player.LastActivity).TotalSeconds <= 240).OrderByDescending(p => p.LastActivity).Select(player => player.Username).ToList();
        }

        private List<string>? GetPlayersInGame(string token)
        {
            return _context.Players.ToList().FindAll(player => player.Token != token && PlayerInGame(player.Token)).OrderByDescending(p => p.LastActivity).Select(player => player.Username).ToList();
        }

        private List<string>? GetFriends(string token)
        {
            return _context.Players.ToList().FirstOrDefault(p => p.Token == token)?.Friends.OrderBy(friend => friend).ToList();
        }

        private List<string>? GetFriendRequests(string token)
        {
            return _context.Players.ToList().FirstOrDefault(p => p.Token == token)?.Requests.Where(r => r.Type == Inquiry.Friend).OrderBy(r => r.Username).Select(p => p.Username).ToList();
        }

        private List<string>? GetGameRequests(string token)
        {
            return _context.Players.ToList().FirstOrDefault(p => p.Token == token)?.Requests?.Where(r => r.Type == Inquiry.Game).OrderByDescending(r => r.Date).Select(p => p.Username).ToList();
        }

        private List<string>? GetSentFriendRequests(string token)
        {
            string? username = GetPlayersName(token);
            return _context.Players.ToList().FindAll(p => p.Requests.Any(r => r.Username == username && r.Type == Inquiry.Friend)).Select(p => p.Username).ToList();
        }

        private List<string>? GetSentGameRequests(string token)
        {
            string? username = GetPlayersName(token);
            return _context.Players.ToList().FindAll(p => p.Requests.Any(r => r.Username == username && r.Type == Inquiry.Game)).Select(p => p.Username).ToList();
        }

        private bool PlayerInGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First == token && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second == token && s.Status != Status.Finished);
            return game is not null;
        }

        private bool IsFriend(string receiver_username, string sender_token)
        {
            var friends = GetFriends(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }

        private bool HasFriendRequest(string receiver_username, string sender_token)
        {
            var friends = GetFriendRequests(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }

        private bool HasSentFriendRequest(string receiver_username, string sender_token)
        {
            var friends = GetSentFriendRequests(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }
    }
}
