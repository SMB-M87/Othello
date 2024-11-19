using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        Task<GameResult?> Get(string token);
        Task<GameResult?> GetLast(string player_token);
        Task<string?> GetPlayerStats(string player_token);
        Task<List<GameResult>> GetResults();
        Task<bool> Delete(string token);
    }
}
