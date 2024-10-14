using Backend.Models;

namespace Backend.Data
{
    public interface IResultRepository
    {
        void Create(GameResult result);
        string GetPlayerStats(string token);
        List<GameResult> GetPlayersMatchHistory(string token);
    }
}
