using API.Models;

namespace API.Data
{
    public interface IPlayerRepository
    {
        void Create(Player player);
        void Update(Player player);
        void Delete(Player player);
        Player? Get(string token);
        Player? GetByUsername(string username);
        bool Exists(string username);
        List<Player>? GetPlayers();
        void SendFriendInvite(string receiver, string sender);
        void AcceptFriendInvite(string receiver, string sender);
        void DeclineFriendInvite(string receiver, string sender);
        void DeleteFriend(string receiver, string sender);
        List<Request>? GetPending(string player);
        List<string>? GetFriends(string player);
        void SendGameInvite(string receiver, string sender);
        void AcceptGameInvite(string receiver, string sender);
        void DeclineGameInvite(string receiver, string sender);
        void DeleteGameInvites(string token);
    }
}
