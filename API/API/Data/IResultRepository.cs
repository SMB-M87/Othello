using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        bool Create(GameResult result);
        GameResult? Get(string token);
        string? GetPlayerStats(string player_token);
        List<GameResult>? GetPlayersMatchHistory(string player_token);
    }
}
