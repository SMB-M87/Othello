using Backend.Models;

namespace Backend.Repositories
{
    public interface IResultRepository
    {
        void Create(GameResult result);
        (int Wins, int Losses, int Draws) GetPlayerStats(string token);
        List<GameResult> GetPlayersMatchHistory(string token);
    }
}
