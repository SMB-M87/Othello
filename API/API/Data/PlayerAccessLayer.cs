using API.Models;

namespace API.Data
{
    public class PlayerAccessLayer : IPlayerRepository
    {
        private readonly Database _context;

        public PlayerAccessLayer(Database context)
        {
            _context = context;
        }

        private bool TokenExists(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token)) != null;
        }

        private bool UsernameExists(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username)) != null;
        }

        public bool Create(Player player)
        {
            if (!TokenExists(player.Token) && !UsernameExists(player.Username))
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private List<Player>? GetPlayers()
        {
            return _context.Players.ToList();
        }

        public List<string>? GetOnlinePlayers()
        {
            return GetPlayers()?.FindAll(player => (DateTime.UtcNow - player.LastActivity).TotalSeconds <= 240).OrderByDescending(p => p.LastActivity).Select(player => player.Username).ToList();
        }

        public List<string>? GetFriends(string token)
        {
            return Get(token)?.Friends.OrderBy(friend => friend).ToList();
        }

        public List<string>? GetFriendRequests(string token)
        {
            return Get(token)?.Requests.Where(r => r.Type == Inquiry.Friend).OrderBy(r => r.Username).Select(p => p.Username).ToList();
        }

        public List<string>? GetGameRequests(string token)
        {
            return Get(token)?.Requests?.Where(r => r.Type == Inquiry.Game).OrderByDescending(r => r.Date).Select(p => p.Username).ToList();
        }

        private string? GetName(string token)
        {
            return Get(token)?.Username;
        }

        public List<string>? GetSentFriendRequests(string token)
        {
            string? username = GetName(token);
            return GetPlayers()?.FindAll(p => p.Requests.Any(r => r.Username == username && r.Type == Inquiry.Friend)).Select(p => p.Username).ToList();
        }

        public List<string>? GetSentGameRequests(string token)
        {
            string? username = GetName(token);
            return GetPlayers()?.FindAll(p => p.Requests.Any(r => r.Username == username && r.Type == Inquiry.Game)).Select(p => p.Username).ToList();
        }

        private Player? Get(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token));
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

        private Player? GetByUsername(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username));
        }

        public bool FriendRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByUsername(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Friend) &&
                !sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                receiver.Requests.Add(new(Inquiry.Friend, sender.Username));
                _context.Entry(receiver).Property(g => g.Requests).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AcceptFriendRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByUsername(receiver_username);
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
            var receiver = GetByUsername(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                !receiver.Friends.Contains(sender.Username) && !sender.Friends.Contains(receiver.Username) &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Friend))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Friend);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
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

        public bool GameRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByUsername(receiver_username);
            var sender = Get(sender_token);
            DateTime now = DateTime.UtcNow;

            if (receiver is not null && sender is not null &&
                !PlayerInGame(receiver.Token) && PlayerInPendingGame(sender.Token) &&
                (now - receiver.LastActivity).TotalSeconds <= 240 &&
                !receiver.Requests.Any(p => p.Username == sender.Username && p.Type == Inquiry.Game))
            {
                receiver.Requests.Add(new(Inquiry.Game, sender.Username));
                _context.Entry(receiver).Property(p => p.Requests).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
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
                    _context.Entry(game).Property(g => g.Status).IsModified = true;
                    _context.Entry(game).Property(g => g.Second).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AcceptGameRequest(string receiver_username, string sender_token)
        {
            var receiver = GetByUsername(receiver_username);
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
            var receiver = GetByUsername(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null &&
                sender.Requests.Any(p => p.Username == receiver.Username && p.Type == Inquiry.Game))
            {
                var request = sender.Requests.FirstOrDefault(p => p.Username == receiver.Username && p.Type == Inquiry.Game);

                if (request is not null)
                {
                    sender.Requests.Remove(request);
                    _context.Entry(sender).Property(p => p.Requests).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteFriend(string receiver_username, string sender_token)
        {
            var receiver = GetByUsername(receiver_username);
            var sender = Get(sender_token);

            if (receiver is not null && sender is not null)
            {
                sender.Friends.Remove(receiver.Username);
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
                return true;
            }
            return false;
        }

        public bool Delete(string token)
        {
            var player = Get(token);

            if (player is not null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
