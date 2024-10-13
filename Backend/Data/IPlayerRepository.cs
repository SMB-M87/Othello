using Backend.Models;

namespace Backend.Data
{
    public interface IPlayerRepository
    {
        void Create(Player player);
        void Update(Player player);
        void Delete(Player player);
        Player? Get(string token);
        Player? GetByUsername(string username);
        string GetName(string token);
        void SendFriendInvite(string receiver, string sender);
        void AcceptFriendInvite(string receiver, string sender);
        void DeclineFriendInvite(string receiver, string sender);
    }
}
