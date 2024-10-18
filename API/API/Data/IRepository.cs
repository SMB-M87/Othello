namespace API.Data
{
    public interface IRepository
    {
        IGameRepository GameRepository { get; }
        IResultRepository ResultRepository { get; }
        IPlayerRepository PlayerRepository { get; }
    }
}
