using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        GameResult? Get(string token);
        GameResult? GetLast(string player_token);
        string? GetPlayerStats(string player_token);
        List<GameResult> GetResults();
        bool Delete(string token);
    }
}
