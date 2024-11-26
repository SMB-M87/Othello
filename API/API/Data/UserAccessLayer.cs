using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserAccessLayer : IUserRepository
    {
        private readonly Database _context;

        public UserAccessLayer(Database context)
        {
            _context = context;
        }

        public async Task<Home> GetView(string token)
        {
            var game = await GetPlayersGame(token);
            var username = await GetPlayersName(token);

            var model = new Home
            {
                Stats = string.IsNullOrEmpty(username) ? string.Empty : await GetPlayerStats(username),
                MatchHistory = string.IsNullOrEmpty(username) ? new() : await GetPlayersMatchHistory(username),
                Partial = new HomePartial()
                {
                    Pending = new HomePending()
                    {
                        Session = "false",
                        InGame = game != null,
                        Status = game != null ? ((int)game.Status).ToString() : string.Empty,
                        Games = await GetPendingGames() ?? new()
                    },

                    OnlinePlayers = await GetOnlinePlayers(token) ?? new(),
                    PlayersInGame = await GetPlayersInGame(token) ?? new(),
                    Friends = await GetFriends(token) ?? new(),

                    FriendRequests = await GetFriendRequests(token) ?? new(),
                    GameRequests = await GetGameRequests(token) ?? new(),

                    SentFriendRequests = await GetSentFriendRequests(token) ?? new(),
                    SentGameRequests = await GetSentGameRequests(token) ?? new()
                }
            };

            return model;
        }

        public async Task<HomePartial> GetPartial(string token)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => (g.First == token) || (g.Second != null && g.Second == token)); ;

            var model = new HomePartial
            {
                Pending = new HomePending()
                {
                    Session = "false",
                    InGame = game != null,
                    Status = game != null ? ((int)game.Status).ToString() : string.Empty,
                    Games = await GetPendingGames() ?? new()
                },

                OnlinePlayers = await GetOnlinePlayers(token) ?? new(),
                PlayersInGame = await GetPlayersInGame(token) ?? new(),
                Friends = await GetFriends(token) ?? new(),

                FriendRequests = await GetFriendRequests(token) ?? new(),
                GameRequests = await GetGameRequests(token) ?? new(),

                SentFriendRequests = await GetSentFriendRequests(token) ?? new(),
                SentGameRequests = await GetSentGameRequests(token) ?? new()
            };

            if (game != null && game.Rematch != null && game.Second == null)
            {
                var player = await GetPlayer(game.Rematch);
                var creator = await GetPlayer(token);

                if (player is not null && creator is not null && !player.Requests.Any(r => r.Username == creator.Username && r.Type == Inquiry.Game))
                {
                    _context.Games.Remove(game);

                    var playersWithGameRequests = await _context.Players
                                                        .FromSqlRaw(@"
                                                            SELECT * 
                                                            FROM Players 
                                                            WHERE JSON_VALUE(Requests, '$[0].Type') = {0}
                                                            AND JSON_VALUE(Requests, '$[0].Username') = {1}",
                                                            (int)Inquiry.Game, creator.Username).ToListAsync();

                    foreach (var gamer in playersWithGameRequests)
                    {
                        var request = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == creator.Username);

                        if (request != null)
                        {
                            gamer.Requests.Remove(request);
                            _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                        }
                    }
                    model.Pending.InGame = false;
                    model.Pending.Status = string.Empty;
                    await _context.SaveChangesAsync();
                }
            }

            return model;
        }

        public async Task<HomeProfile> GetProfile(string username, string token)
        {
            HomeProfile profile = new()
            {
                Stats = await GetPlayerStats(username) ?? string.Empty,
                Username = username ?? string.Empty,
                MatchHistory = username != null ? await GetPlayersMatchHistory(username) : new(),
                IsFriend = username != null && await IsFriend(username, token),
                HasSentRequest = username != null && await HasSentFriendRequest(username, token),
                HasPendingRequest = username != null && await HasFriendRequest(username, token),
                LastSeen = await GetLastActivity(username ?? string.Empty)
            };

            return profile;
        }

        private async Task<Player?> GetPlayer(string token)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Token == token);
        }

        private async Task<string?> GetPlayersName(string token)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Token == token);
            return player?.Username;
        }

        private async Task<string?> GetPlayersToken(string username)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Username == username);
            return player?.Token;
        }

        public async Task<Game?> GetPlayersGame(string token)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => (g.First == token) || (g.Second != null && g.Second == token));
        }

        public async Task<string> GetPlayerStats(string username)
        {
            var token = await GetPlayersToken(username);

            if (token != null)
            {
                List<GameResult> results = await GetMatchHistory(token);

                int wins = results.Count(r => r.Winner == token && r.Draw == false);
                int losses = results.Count(r => r.Loser == token && r.Draw == false);
                int draws = results.Count(r => (r.Winner == token || r.Loser == token) && r.Draw == true);

                return $"Wins:{wins}\t\tLosses:{losses}\t\tDraws:{draws}";
            }
            return string.Empty;
        }

        private async Task<List<GameResult>> GetMatchHistory(string token)
        {
            return await _context.Results.AsNoTracking()
                .Where(s => s.Winner == token || s.Loser == token)
                .ToListAsync();
        }

        private async Task<List<GameResult>> GetPlayersMatchHistory(string username)
        {
            var token = await GetPlayersToken(username);

            if (token is not null)
            {
                var response = await GetMatchHistory(token);

                if (response.Count > 0)
                {
                    response = response.OrderByDescending(r => r.Date).ToList();

                    foreach (var game in response)
                    {
                        game.Winner = await GetPlayersName(game.Winner) ?? string.Empty;
                        game.Loser = await GetPlayersName(game.Loser) ?? string.Empty;
                    }
                }
                return response;
            }
            return new();
        }

        private async Task<List<GamePending>?> GetPendingGames()
        {
            var games = await _context.Games.AsNoTracking().Where(g => g.Status == Status.Pending && g.Second == null && g.Rematch == null).OrderByDescending(g => g.Date).ToListAsync();

            if (games is not null)
            {
                List<GamePending> result = new();

                foreach (Game game in games)
                {
                    var player = await GetPlayersName(game.First) ?? string.Empty;
                    var stat = await GetPlayerStats(player) ?? string.Empty;
                    string output = stat.Replace("Wins:", "W:")
                                        .Replace("Losses:", "L:")
                                        .Replace("Draws:", "D:")
                                        .Replace("\t\t", " ");

                    GamePending temp = new(game.Description, player, output);
                    result.Add(temp);
                }
                return result;
            }
            return null;
        }

        private async Task<string> GetLastActivity(string username)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Username == username);
            DateTime? lastActivity = player?.LastActivity;

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

        private async Task<List<string>?> GetOnlinePlayers(string token)
        {
            DateTime threshold = DateTime.UtcNow.AddSeconds(-240);
            return await _context.Players.AsNoTracking()
                .Where(p => p.Token != token && p.LastActivity >= threshold)
                .OrderByDescending(p => p.LastActivity)
                .Select(p => p.Username)
                .ToListAsync();
        }

        private async Task<List<string>?> GetPlayersInGame(string token)
        {
            DateTime threshold = DateTime.UtcNow.AddSeconds(-240);
            var players = await _context.Players.AsNoTracking()
                .Where(p => p.Token != token && p.LastActivity >= threshold)
                .OrderByDescending(p => p.LastActivity)
                .ToListAsync();

            var playersInGame = new List<string>();

            foreach (var player in players)
            {
                if (await PlayerInGame(player.Token))
                    playersInGame.Add(player.Username);
            }

            return playersInGame;
        }

        private async Task<List<string>?> GetFriends(string token)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Token == token);
            return player?.Friends.OrderBy(friend => friend).ToList();
        }

        private async Task<List<string>?> GetFriendRequests(string token)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Token == token);
            return player?.Requests.Where(r => r.Type == Inquiry.Friend).OrderBy(r => r.Username).Select(p => p.Username).ToList();
        }

        private async Task<List<string>?> GetGameRequests(string token)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Token == token);
            return player?.Requests.Where(r => r.Type == Inquiry.Game).OrderByDescending(r => r.Date).Select(p => p.Username).ToList();
        }

        private async Task<List<string>?> GetSentFriendRequests(string token)
        {
            string? username = await GetPlayersName(token);

            if (string.IsNullOrEmpty(username))
                return new();

            return await _context.Players
                                 .FromSqlRaw(@"
                                     SELECT * 
                                     FROM Players 
                                     WHERE JSON_VALUE(Requests, '$[0].Type') = {0}
                                     AND JSON_VALUE(Requests, '$[0].Username') = {1}",
                                 (int)Inquiry.Friend, username).Select(p => p.Username).AsNoTracking().ToListAsync();
        }

        private async Task<List<string>?> GetSentGameRequests(string token)
        {
            string? username = await GetPlayersName(token);

            if (string.IsNullOrEmpty(username))
                return new();

            return await _context.Players
                                 .FromSqlRaw(@"
                                     SELECT * 
                                     FROM Players 
                                     WHERE JSON_VALUE(Requests, '$[0].Type') = {0}
                                     AND JSON_VALUE(Requests, '$[0].Username') = {1}",
                                     (int)Inquiry.Game, username).Select(p => p.Username).AsNoTracking().ToListAsync();
        }

        private async Task<bool> PlayerInGame(string token)
        {
            var game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => (g.First == token) || (g.Second != null && g.Second == token));
            return game is not null;
        }

        private async Task<bool> IsFriend(string receiver_username, string sender_token)
        {
            var friends = await GetFriends(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }

        private async Task<bool> HasFriendRequest(string receiver_username, string sender_token)
        {
            var friends = await GetFriendRequests(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }

        private async Task<bool> HasSentFriendRequest(string receiver_username, string sender_token)
        {
            var friends = await GetSentFriendRequests(sender_token);
            return friends?.Contains(receiver_username) ?? false;
        }
    }
}
