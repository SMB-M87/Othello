using API.Models;

namespace API.Data
{
    public interface IPlayerRepository
    {
        Player? Get(string token);
        Player? GetByName(string username);
        string? GetRematch(string receiver_username, string sender_token);
        bool UsernameExists(string username);
        List<Player> GetPlayers();
        bool Create(Player player);
        bool UpdateActivity(string token);
        bool FriendRequest(string receiver_username, string sender_token);
        bool AcceptFriendRequest(string receiver_username, string sender_token);
        bool DeclineFriendRequest(string receiver_username, string sender_token);
        bool GameRequest(string receiver_username, string sender_token);
        bool AcceptGameRequest(string receiver_username, string sender_token);
        bool DeclineGameRequest(string receiver_username, string sender_token);
        bool DeleteFriend(string receiver_username, string sender_token);
        bool DeleteGameInvites(string token);
        bool Delete(string token);
    }
}
