using Backend.Models;

namespace Backend.Repositories
{
    public interface IPlayerRepository
    {
        void Create(Player player);
        void Update(Player player);
        void Delete(Player player);
        Player? Get(string token);
        Player? GetByUsername(string username);
        string GetName(string token);
        void SendFriendInvite(string username, string sender);
        void AcceptFriendInvite(string username, string sender);
        void DeclineFriendInvite(string username, string sender);
    }
}
