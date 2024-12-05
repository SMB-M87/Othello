using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PlayerAccessLayer : IPlayerRepository
    {
        private readonly Database _context;

        public PlayerAccessLayer(Database context)
        {
            _context = context;
        }

        public async Task<Player?> Get(string token)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Token.Equals(token));
        }

        public async Task<Player?> GetByName(string username)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Username.Equals(username));
        }

        public async Task<string?> GetRematch(string receiver_username, string sender_token)
        {
            var player = await Get(sender_token);

            if (player == null) return null;

            var request = player.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == receiver_username);

            if (request == null) return null;

            return request.Username;
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Username.Equals(username)) != null;
        }

        public async Task<bool> PlayerChecksOut(string token, string username)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Username.Equals(username) && s.Token.Equals(token)) != null;
        }

        public async Task<List<Player>> GetPlayers()
        {
            return await _context.Players.AsNoTracking().ToListAsync();
        }

        public async Task<List<string>> GetInactivePlayers()
        {
            var threshold = DateTime.UtcNow.AddMinutes(-5);
            var maxThreshold = DateTime.UtcNow.AddMinutes(-8);

            var players = await _context.Players.AsNoTracking().Where(p => p.LastActivity >= maxThreshold && p.LastActivity <= threshold && p.Bot == 0).ToListAsync();

            List<string> result = new();

            foreach (var player in players)
            {
                if (player.Username != "Deleted")
                    result.Add(player.Username);
            }

            return result;
        }

        public async Task<bool> Create(Player player)
        {
            if (!await TokenExists(player.Token) && !await UsernameExists(player.Username))
            {
                await _context.Players.AddAsync(player);
                await UpdateActivity(player.Token);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateActivity(string token)
        {
            if (await TokenExists(token))
            {
                var player = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(token));

                if (player is not null)
                {
                    player.LastActivity = DateTime.UtcNow;
                    _context.Entry(player).Property(g => g.LastActivity).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> FriendRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Friend) &&
                !sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                receiver.Requests.Add(new(Inquiry.Friend, sender.Username));
                await UpdateActivity(sender_token);
                _context.Entry(receiver).Property(g => g.Requests).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AcceptFriendRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Friend);

                if (request is not null)
                {
                    receiver.Friends.Add(sender.Username);
                    _context.Entry(receiver).Property(p => p.Friends).IsModified = true;

                    sender.Requests.Remove(request);
                    sender.Friends.Add(receiver.Username);
                    await UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Friends).IsModified = true;
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeclineFriendRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Friend);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    await UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> GameRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));
            DateTime now = DateTime.UtcNow;

            if (receiver is not null && sender is not null &&
                !await PlayerInGame(receiver.Token) && await PlayerInPendingGame(sender.Token) &&
                (now - receiver.LastActivity).TotalSeconds <= 240 &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Game))
            {
                receiver.Requests.Add(new(Inquiry.Game, sender.Username));
                await UpdateActivity(sender_token);
                _context.Entry(receiver).Property(p => p.Requests).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AcceptGameRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));
            DateTime now = DateTime.UtcNow;

            if (receiver is not null && sender is not null &&
                await PlayerInPendingGame(receiver.Token) && !await PlayerInGame(sender.Token) &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Game))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Game);

                if (request is not null && (now - request.Date).TotalSeconds <= 76)
                {
                    await JoinPlayersGame(receiver.Token, sender.Token);
                    sender.Requests.Remove(request);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeclineGameRequest(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));

            if (receiver is not null && sender is not null &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Game))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Game);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    await UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteFriend(string receiver_username, string sender_token)
        {
            var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(receiver_username));
            var sender = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(sender_token));

            if (receiver is not null && sender is not null)
            {
                sender.Friends.Remove(receiver.Username);
                await UpdateActivity(sender_token);
                _context.Entry(sender).Property(p => p.Friends).IsModified = true;
                receiver.Friends.Remove(sender.Username);
                _context.Entry(receiver).Property(p => p.Friends).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string token)
        {
            var player = await _context.Players.FirstOrDefaultAsync(s => s.Token.Equals(token));

            if (player is not null)
            {
                var allPlayers = await _context.Players.ToListAsync();
                var game = await _context.Games.FirstOrDefaultAsync(s => s.Second != null && s.Second == token || s.First == token);

                if (game is not null)
                {
                    if (game.Second is not null)
                    {
                        string winner;
                        string loser;

                        if (game.First == token)
                        {
                            winner = game.Second;
                            loser = "deleted";
                        }
                        else
                        {
                            winner = game.First;
                            loser = "deleted";
                        }
                        GameResult result = new(game.Token, winner, loser, game.Board)
                        {
                            Date = DateTime.UtcNow
                        };
                        await _context.Results.AddAsync(result);
                        _context.Games.Remove(game);
                    }
                    else
                    {
                        _context.Games.Remove(game);
                        var playersWithGameRequests = allPlayers.Where(p => p.Requests.Any(r => r.Username == player.Username && r.Type == Inquiry.Game)).ToList();

                        foreach (var gamer in playersWithGameRequests)
                        {
                            var request = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == player.Username);

                            if (request != null)
                            {
                                gamer.Requests.Remove(request);
                                _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();

                var requestsToRemove = player.Requests.ToList();
                foreach (Request request in requestsToRemove)
                {
                    player.Requests.Remove(request);
                }
                await _context.SaveChangesAsync();

                var friendsToRemove = player.Friends.ToList();
                foreach (string friend in friendsToRemove)
                {
                    var receiver = await _context.Players.FirstOrDefaultAsync(s => s.Username.Equals(friend));

                    if (receiver is not null)
                    {
                        player.Friends.Remove(receiver.Username);
                        _context.Entry(player).Property(p => p.Friends).IsModified = true;
                        receiver.Friends.Remove(player.Username);
                        _context.Entry(receiver).Property(p => p.Friends).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();

                var players = allPlayers.Where(p => p.Requests.Any(r => r.Username == player.Username)).ToList();

                foreach (Player gamer in players)
                {
                    var requestFriend = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Friend && r.Username == player.Username);
                    if (requestFriend != null)
                    {
                        gamer.Requests.Remove(requestFriend);
                        _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                    }

                    var requestGame = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == player.Username);

                    if (requestGame != null)
                    {
                        gamer.Requests.Remove(requestGame);
                        _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();

                var allResults = await _context.Results.ToListAsync();
                var results = allResults.Where(s => s.Winner == token || s.Loser == token).ToList();

                foreach (GameResult result in results)
                {
                    if (result.Winner == player.Token)
                    {
                        result.Winner = "deleted";
                        _context.Entry(result).Property(p => p.Winner).IsModified = true;
                    }
                    else
                    {
                        result.Loser = "deleted";
                        _context.Entry(result).Property(p => p.Loser).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();

                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> PlayerInGame(string token)
        {
            var game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.First.Equals(token) || (g.Second != null && g.Second.Equals(token)));
            return game is not null;
        }

        private async Task<bool> PlayerInPendingGame(string token)
        {
            var game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(s => s.First.Equals(token) && s.Second == null && s.Status == Status.Pending);
            return game is not null;
        }

        private async Task<bool> JoinPlayersGame(string receiver_token, string sender_token)
        {
            var game = await _context.Games.FirstOrDefaultAsync(s => s.First.Equals(receiver_token) && s.Status == Status.Pending);

            if (game is not null)
            {
                game.SetSecondPlayer(sender_token);
                await UpdateActivity(game.First);
                await UpdateActivity(sender_token);
                _context.Entry(game).Property(g => g.Status).IsModified = true;
                _context.Entry(game).Property(g => g.Second).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> TokenExists(string token)
        {
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(s => s.Token == token) != null;
        }
    }
}
