using API.Models;

namespace API.Data
{
    public interface IPlayerRepository
    {
        Task<Player?> Get(string token);
        Task<Player?> GetByName(string username);
        Task<string?> GetRematch(string receiver_username, string sender_token);
        Task<bool> UsernameExists(string username);
        Task<bool> PlayerChecksOut(string token, string username);
        Task<List<Player>> GetPlayers();
        Task<bool> Create(Player player);
        Task<bool> UpdateActivity(string token);
        Task<bool> FriendRequest(string receiver_username, string sender_token);
        Task<bool> AcceptFriendRequest(string receiver_username, string sender_token);
        Task<bool> DeclineFriendRequest(string receiver_username, string sender_token);
        Task<bool> GameRequest(string receiver_username, string sender_token);
        Task<bool> AcceptGameRequest(string receiver_username, string sender_token);
        Task<bool> DeclineGameRequest(string receiver_username, string sender_token);
        Task<bool> DeleteFriend(string receiver_username, string sender_token);
        Task<bool> Delete(string token);
    }
}
