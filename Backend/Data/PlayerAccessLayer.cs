using Backend.Models;

namespace Backend.Data
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
                update.Username = player.Username;
                update.Friends = player.Friends;
                update.PendingFriends = player.PendingFriends;
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

        public Player? Get(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token));
        }

        public Player? GetByUsername(string username)
        {
            return _context.Players.FirstOrDefault(s => s.Username.Equals(username));
        }

        public string GetName(string token)
        {
            var player = _context.Players.FirstOrDefault(s => s.Token.Equals(token));
            return player?.Username ?? "";
        }

        public void SendFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                receiver_control.PendingFriends.Add(sender);
                sender_control.PendingFriends.Add(receiver);
                _context.SaveChanges();
            }
        }

        public void AcceptFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                receiver_control.Friends.Add(sender);
                receiver_control.PendingFriends.Remove(sender);
                sender_control.Friends.Add(receiver);
                sender_control.PendingFriends.Remove(receiver);
                _context.SaveChanges();
            }
        }

        public void DeclineFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                receiver_control.PendingFriends.Remove(sender);
                sender_control.PendingFriends.Remove(receiver);
                _context.SaveChanges();
            }
        }
    }
}
