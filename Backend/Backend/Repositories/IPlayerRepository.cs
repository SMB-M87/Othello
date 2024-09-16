using Backend.Models;

namespace Backend.Repositories
{
    public interface IPlayerRepository
    {
        void AddPlayer(Player player);
        void UpdatePlayer(Player player);
        void DeletePlayer(Player player);
        Player? GetPlayer(string token);
        Player? GetPlayerByUsername(string username);
        string GetPlayersName(string token);
        void SendFriendInvite(string username, string sender);
        void AcceptFriendInvite(string username, string sender);
        void DeclineFriendInvite(string username, string sender);
    }
}
