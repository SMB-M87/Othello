/*using Backend.Models;

namespace Backend.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRepository _repository;

        public PlayerRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Create(Player player)
        {
            _repository.Players().Add(player);
        }

        public void Update(Player player)
        {
            int index = _repository.Players().FindIndex(s => s.Token.Equals(player.Token));

            if (index != -1)
                _repository.Players()[index] = player;
        }

        public void Delete(Player player)
        {
            _repository.Players().Remove(player);
        }

        public Player? Get(string token)
        {
            return _repository.Players().Find(s => s.Token.Equals(token, StringComparison.Ordinal));
        }

        public Player? GetByUsername(string username)
        {
            return _repository.Players().Find(s => s.Username.Equals(username, StringComparison.Ordinal));
        }

        public string GetName(string token)
        {
            Player? player = _repository.Players().Find(s => s.Token.Equals(token, StringComparison.Ordinal));

            if (player is not null)
                return player.Username;
            else
                return string.Empty;
        }

        public void SendFriendInvite(string receiver, string sender)
        {
            Player? player_receiver = _repository.Players().Find(s => s.Username.Equals(receiver, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if (player_receiver is not null && player_sender is not null)
            {
                player_receiver.PendingFriends.Add(sender);
                player_sender.PendingFriends.Add(receiver);
            }
        }

        public void AcceptFriendInvite(string receiver, string sender)
        {
            Player? player_receiver = _repository.Players().Find(s => s.Username.Equals(receiver, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if (player_receiver is not null && player_sender is not null)
            {
                player_receiver.PendingFriends.Remove(sender);
                player_sender.PendingFriends.Remove(receiver);
                player_receiver.Friends.Add(sender);
                player_sender.Friends.Add(receiver);
            }
        }

        public void DeclineFriendInvite(string receiver, string sender)
        {
            Player? player_receiver = _repository.Players().Find(s => s.Username.Equals(receiver, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if (player_receiver is not null && player_sender is not null)
            {
                player_receiver.PendingFriends.Remove(sender);
                player_sender.PendingFriends.Remove(receiver);
            }
        }
    }
}
*/