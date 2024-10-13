using Backend.Models;

namespace Backend.Data
{
    public interface IRepository
    {
        IGameRepository GameRepository { get; }
        IResultRepository ResultRepository { get; }
        IPlayerRepository PlayerRepository { get; }
    }
}
