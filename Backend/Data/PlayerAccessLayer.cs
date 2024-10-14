using Backend.Models;
using System.Numerics;

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
                update.Friends = player.Friends;
                _context.Entry(update).Property(g => g.Friends).IsModified = true;
                update.PendingFriends = player.PendingFriends;
                _context.Entry(update).Property(g => g.PendingFriends).IsModified = true;
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

        public string GetName(string token)
        {
            var player = _context.Players.FirstOrDefault(s => s.Token.Equals(token));
            return player?.Username ?? "";
        }

        public void SendFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null && !receiver_control.PendingFriends.Contains(sender))
            {
                receiver_control.PendingFriends.Add(sender);
                _context.SaveChanges();
            }
        }

        public void AcceptFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null && !receiver_control.Friends.Contains(sender))
            {
                receiver_control.Friends.Add(sender);
                sender_control.PendingFriends.Remove(receiver);
                sender_control.Friends.Add(receiver);
                _context.SaveChanges();
            }
        }

        public void DeclineFriendInvite(string receiver, string sender)
        {
            var receiver_control = GetByUsername(receiver);
            var sender_control = GetByUsername(sender);

            if (receiver_control is not null && sender_control is not null)
            {
                sender_control.PendingFriends.Remove(receiver);
                _context.SaveChanges();
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

        public List<string>? GetPending(string player)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(player))?.PendingFriends;
        }

        public List<string>? GetFriends(string player)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(player))?.Friends;
        }
    }
}
