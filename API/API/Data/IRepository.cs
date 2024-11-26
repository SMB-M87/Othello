namespace API.Data
{
    public interface IRepository
    {
        IGameRepository GameRepository { get; }
        IResultRepository ResultRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IUserRepository HomeRepository { get; }
        ILogRepository LogRepository { get; }
    }
}
