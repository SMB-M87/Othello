using API.Models;

namespace API.Data
{
    public interface IPlayerRepository
    {
        bool Create(Player player);
        List<string>? GetOnlinePlayers();
        List<string>? GetPlayersInGame();
        bool UsernameExists(string username);
        List<string>? GetFriends(string token);
        List<string>? GetFriendRequests(string token);
        List<string>? GetGameRequests(string token);
        List<string>? GetSentFriendRequests(string token);
        List<string>? GetSentGameRequests(string token);
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
