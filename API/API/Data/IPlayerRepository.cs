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
        string GetName(string token);
        bool Exists(string username);
        void SendFriendInvite(string receiver, string sender);
        void AcceptFriendInvite(string receiver, string sender);
        void DeclineFriendInvite(string receiver, string sender);
        void DeleteFriend(string receiver, string sender);
        List<string>? GetPending(string player);
        List<string>? GetFriends(string player);
    }
}
