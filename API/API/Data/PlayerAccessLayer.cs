using API.Models;
using System.Numerics;

namespace API.Data
{
    public class PlayerAccessLayer : IPlayerRepository
    {
        private readonly Database _context;

        public PlayerAccessLayer(Database context)
        {
            _context = context;
        }

        public Player? Get(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token));
        }

        public Player? GetByName(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username));
        }

        public string? GetRematch(string receiver_username, string sender_token)
        {
            var player = Get(sender_token);

            if (player == null) return null;

            var request = player.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == receiver_username);

            if (request == null) return null;

            return request.Username;
        }

        public bool UsernameExists(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username)) != null;
        }

        public List<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public bool Create(Player player)
        {
            if (!TokenExists(player.Token) && !UsernameExists(player.Username))
            {
                _context.Players.Add(player);
                UpdateActivity(player.Token);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateActivity(string token)
        {
            if (TokenExists(token))
            {
                var player = Get(token);

                if (player is not null)
                {
                    player.LastActivity = DateTime.UtcNow;
                    _context.Entry(player).Property(g => g.LastActivity).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool FriendRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Friend) &&
                !sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                receiver.Requests.Add(new(Inquiry.Friend, sender.Username));
                UpdateActivity(sender_token);
                _context.Entry(receiver).Property(g => g.Requests).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AcceptFriendRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);

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
                    UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Friends).IsModified = true;
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeclineFriendRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Friend);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool GameRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);
            DateTime now = DateTime.UtcNow;

            if (receiver is not null && sender is not null &&
                !PlayerInGame(receiver.Token) && PlayerInPendingGame(sender.Token) &&
                (now - receiver.LastActivity).TotalSeconds <= 240 &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Game))
            {
                receiver.Requests.Add(new(Inquiry.Game, sender.Username));
                UpdateActivity(sender_token);
                _context.Entry(receiver).Property(p => p.Requests).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AcceptGameRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);
            DateTime now = DateTime.UtcNow;

            if (receiver is not null && sender is not null &&
                PlayerInPendingGame(receiver.Token) && !PlayerInGame(sender.Token) &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Game))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Game);

                if (request is not null && (now - request.Date).TotalSeconds <= 59)
                {
                    JoinPlayersGame(receiver.Token, sender.Token);
                    sender.Requests.Remove(request);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeclineGameRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Game))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Game);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    UpdateActivity(sender_token);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteFriend(string receiver_username, string sender_token)
        {
            var receiver = GetByName(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null)
            {
                sender.Friends.Remove(receiver.Username);
                UpdateActivity(sender_token);
                _context.Entry(sender).Property(p => p.Friends).IsModified = true;
                receiver.Friends.Remove(sender.Username);
                _context.Entry(receiver).Property(p => p.Friends).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteGameInvites(string token)
        {
            var player = Get(token);

            if (player is not null)
            {
                var expiredRequests = player.Requests
                    .Where(request => request.Type == Inquiry.Game && (DateTime.UtcNow - request.Date).TotalSeconds >= 60)
                    .ToList();

                foreach (var expiredRequest in expiredRequests)
                {
                    player.Requests.Remove(expiredRequest);
                }
                _context.Entry(player).Property(p => p.Requests).IsModified = true;
                _context.SaveChanges();

                var players = _context.Players.ToList().FindAll(p => p.Requests.Any(r => r.Username == player.Username && r.Type == Inquiry.Game)).ToList();

                foreach (Player gamer in players)
                {
                    var request = gamer.Requests.FirstOrDefault(r => r.Type == Inquiry.Game && r.Username == player.Username && (DateTime.UtcNow - r.Date).TotalSeconds >= 60);

                    if (request != null)
                    {
                        gamer.Requests.Remove(request);
                        _context.Entry(gamer).Property(p => p.Requests).IsModified = true;
                        _context.SaveChanges();
                    }
                }
                UpdateActivity(token);
                return true;
            }
            return false;
        }

        public bool Delete(string token)
        {
            var player = Get(token);

            if (player is not null)
            {
                var requestsToRemove = player.Requests.ToList();
                foreach (Request request in requestsToRemove)
                {
                    player.Requests.Remove(request);
                }

                var friendsToRemove = player.Friends.ToList();
                foreach (string friend in friendsToRemove)
                {
                    DeleteFriend(friend, player.Token);
                }

                var players = _context.Players.ToList().FindAll(p => p.Requests.Any(r => r.Username == player.Username)).ToList();
                
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
                    _context.SaveChanges();
                }

                var results = _context.Results.ToList().FindAll(r => r.Winner == player.Token || r.Loser == player.Token).ToList();

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
                    _context.SaveChanges();
                }

                _context.Players.Remove(player);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool PlayerInGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First.Equals(token) && s.Status != Status.Finished);
            game ??= games!.FirstOrDefault(s => s.Second != null && s.Second.Equals(token) && s.Status != Status.Finished);
            return game is not null;
        }

        private bool PlayerInPendingGame(string token)
        {
            var games = _context.Games.ToList();
            var game = games!.FirstOrDefault(s => s.First.Equals(token) && s.Second is null && s.Status == Status.Pending);
            return game is not null;
        }

        private bool JoinPlayersGame(string receiver_token, string sender_token)
        {
            List<Game>? games = _context.Games.ToList();

            if (games.Count > 0)
            {
                var game = games!.FirstOrDefault(s => s.First.Equals(receiver_token) && s.Status != Status.Finished);

                if (game is not null)
                {
                    game.SetSecondPlayer(sender_token);
                    UpdateActivity(game.First);
                    UpdateActivity(sender_token);
                    _context.Entry(game).Property(g => g.Status).IsModified = true;
                    _context.Entry(game).Property(g => g.Second).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private bool TokenExists(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token == token) != null;
        }
    }
}
