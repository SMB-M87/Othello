using API.Models;

namespace API.Data
{
    public interface IGameRepository
    {
        void Create(Game game);
        void Join(GameEntrant entry);
        void JoinPlayer(GameEntrant entry);
        void Update(Game game);
        void Delete(Game game);
        Game? Get(string token);
        Game? GetPlayersGame(string token);
        List<Game>? GetGames();
    }
}
