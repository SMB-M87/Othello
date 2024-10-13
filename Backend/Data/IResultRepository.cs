using Backend.Models;

namespace Backend.Data
{
    public interface IResultRepository
    {
        void Create(GameResult result);
        (int Wins, int Losses, int Draws) GetPlayerStats(string token);
        List<GameResult> GetPlayersMatchHistory(string token);
    }
}
