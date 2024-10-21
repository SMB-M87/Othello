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

        public void Create(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void Update(Player player)
        {
            var update = Get(player.Token);

            if (update is not null)
            {
                update.LastActivity = player.LastActivity;
                _context.Entry(update).Property(g => g.LastActivity).IsModified = true;
                update.Friends = player.Friends;
                _context.Entry(update).Property(g => g.Friends).IsModified = true;
                update.Requests = player.Requests;
                _context.Entry(update).Property(g => g.Requests).IsModified = true;
                _context.SaveChanges();
            }
        }

        public void Delete(Player player)
        {
            var remove = Get(player.Token);

            if (remove is not null)
            {
                _context.Players.Remove(remove);
                _context.SaveChanges();
            }
        }

        public Player? Get(string player)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(player));
        }

        public Player? GetByUsername(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username));
        }

        public bool Exists(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username)) != null;
        }

        public List<Player>? GetPlayers()
        {
            return _context.Players.ToList();
        }

        public List<string>? GetFriends(string player)
        {
            return (List<string>?)(_context.Players.FirstOrDefault(s => s.Token.Equals(player))?.Friends);
        }

        public void SendFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null && !receiver_control.Friends.Contains(sender) && 
                !receiver_control.Requests.Any(p => p.Username == sender && p.Type == Inquiry.Friend))
            {
                receiver_control.Requests.Add(new(Inquiry.Friend, sender));
                _context.SaveChanges();
            }
        }

        public void AcceptFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null && !receiver_control.Friends.Contains(sender))
            {
                var control = sender_control.Requests.FirstOrDefault(p => p.Username == receiver && p.Type == Inquiry.Friend);

                if (control is not null)
                {
                    receiver_control.Friends.Add(sender);
                    sender_control.Requests.Remove(control);
                    sender_control.Friends.Add(receiver);
                    _context.SaveChanges();
                }
            }
        }

        public void DeclineFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                var control = sender_control.Requests.FirstOrDefault(p => p.Username == receiver && p.Type == Inquiry.Friend);

                if (control is not null)
                {
                    sender_control.Requests.Remove(control);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteFriend(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                sender_control.Friends.Remove(receiver);
                receiver_control.Friends.Remove(sender);
                _context.SaveChanges();
            }
        }

        public List<Request>? GetPending(string player)
        {
            return (List<Request>?)(_context.Players.FirstOrDefault(s => s.Token.Equals(player))?.Requests);
        }

        public void SendGameInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);
            DateTime now = DateTime.UtcNow;

            if (receiver_control is not null && sender_control is not null && (now - receiver_control.LastActivity).TotalSeconds <= 240
                && !receiver_control.Requests.Any(p => p.Username == sender && p.Type == Inquiry.Game))
            {
                var game_control = _context.Games.FirstOrDefault(g => g.First == receiver_control.Token || g.Second == receiver_control.Token);

                if (game_control is null)
                {
                    receiver_control.Requests.Add(new(Inquiry.Game, sender));
                    _context.SaveChanges();
                }
            }
        }

        public void AcceptGameInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                var control = sender_control.Requests.FirstOrDefault(p => p.Username == receiver && p.Type == Inquiry.Game);
                var game_control = _context.Games.FirstOrDefault(g => g.First == receiver_control.Token && g.Second == string.Empty);

                if (control is not null && game_control is not null)
                {
                    receiver_control.Friends.Add(sender);
                    sender_control.Requests.Remove(control);
                    sender_control.Friends.Add(receiver);
                    _context.SaveChanges();
                }
            }
        }

        public void DeclineGameInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                var control = sender_control.Requests.FirstOrDefault(p => p.Username == receiver && p.Type == Inquiry.Game);

                if (control is not null)
                {
                    sender_control.Requests.Remove(control);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteGameInvites(string token)
        {
            var player = Get(token);

            if (player is not null)
            {
                var expiredRequests = player.Requests
                    .Where(request => request.Type == Inquiry.Game && (DateTime.UtcNow - request.Date).TotalSeconds >= 30)
                    .ToList();

                foreach (var expiredRequest in expiredRequests)
                {
                    player.Requests.Remove(expiredRequest);
                }

                _context.SaveChanges();
            }
        }
    }
}
