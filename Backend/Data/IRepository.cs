using Backend.Models;

namespace Backend.Data
{
    public interface IRepository
    {
        IGameRepository GameRepository { get; }
        List<Game> Games();
        IResultRepository ResultRepository { get; }
        List<GameResult> Results();
        IPlayerRepository PlayerRepository { get; }
        List<Player> Players();
    }
}
