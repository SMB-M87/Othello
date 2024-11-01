using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        bool Create(GameResult result);
        GameResult? Get(string token);
        List<GameResult> GetResults();
        string? GetPlayerStats(string player_token);
        List<GameResult>? GetPlayersMatchHistory(string player_token);
        bool Delete(string token);
    }
}
