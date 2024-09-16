using Backend.Models;

namespace Backend.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRepository _repository;

        public PlayerRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void AddPlayer(Player player)
        {
            _repository.Players().Add(player);
        }

        public void UpdatePlayer(Player player)
        {
            int index = _repository.Players().FindIndex(s => s.Token.Equals(player.Token));

            if (index != -1)
                _repository.Players()[index] = player;
        }

        public void DeletePlayer(Player player)
        {
            _repository.Players().Remove(player);
        }

        public Player? GetPlayer(string token)
        {
            return _repository.Players().Find(s => s.Token.Equals(token, StringComparison.Ordinal));
        }

        public Player? GetPlayerByUsername(string username)
        {
            return _repository.Players().Find(s => s.Username.Equals(username, StringComparison.Ordinal));
        }

        public string GetPlayersName(string token)
        {
            Player? player = _repository.Players().Find(s => s.Token.Equals(token, StringComparison.Ordinal));

            if (player is not null)
                return player.Username;
            else
                return string.Empty;
        }

        public void SendFriendInvite(string username, string sender)
        {
            Player? player = _repository.Players().Find(s => s.Username.Equals(username, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if(player is not null && player_sender is not null)
            {
                player.PendingFriends.Add(sender);
                player_sender.PendingFriends.Add(username);
            }
        }

        public void AcceptFriendInvite(string username, string sender)
        {
            Player? player = _repository.Players().Find(s => s.Username.Equals(username, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if (player is not null && player_sender is not null)
            {
                player.PendingFriends.Remove(sender);
                player_sender.PendingFriends.Remove(username);
                player.Friends.Add(sender);
                player_sender.Friends.Add(username);
            }
        }

        public void DeclineFriendInvite(string username, string sender)
        {
            Player? player = _repository.Players().Find(s => s.Username.Equals(username, StringComparison.Ordinal));
            Player? player_sender = _repository.Players().Find(s => s.Username.Equals(sender, StringComparison.Ordinal));

            if (player is not null && player_sender is not null)
            {
                player.PendingFriends.Remove(sender);
                player_sender.PendingFriends.Remove(username);
            }
        }
    }
}
