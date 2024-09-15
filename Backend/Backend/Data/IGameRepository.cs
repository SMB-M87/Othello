using Backend.Models;

namespace Backend.Data
{
    public interface IGameRepository
    {
        Game? GetPlayersGame(string playerToken);
        Game? GetGame(string token);
        List<Game>? GetGames();
        void AddPlayer(Player player);
        void AddGame(Game game);
        void AddResult(GameResult result);
        void JoinGame(GameEntrant entry);
        void UpdatePlayer(Player player);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
    }
}
