using API.Models;

namespace API.Data
{
    public interface IResultRepository
    {
        void Create(GameResult result);
        GameResult? Get(string token);
        string GetPlayerStats(string token);
        List<GameResult> GetPlayersMatchHistory(string token);
    }
}
