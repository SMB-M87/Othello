using Backend.Models;

namespace Backend.Repositories
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        void JoinGame(GameEntrant entry);
        void JoinPlayer(GameEntrant entry);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        Game? GetGame(string token);
        Game? GetPlayersGame(string token);
        List<Game>? GetGames();
    }
}
