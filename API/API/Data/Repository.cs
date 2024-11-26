namespace API.Data
{
    public class Repository : IRepository
    {
        private readonly Database _context;

        public Repository(Database context)
        {
            _context = context;
        }

        public IGameRepository GameRepository => new GameAccessLayer(_context);
        public IResultRepository ResultRepository => new ResultAccessLayer(_context);
        public IPlayerRepository PlayerRepository => new PlayerAccessLayer(_context);
        public IUserRepository HomeRepository => new UserAccessLayer(_context);
        public ILogRepository LogRepository => new LogAccessLayer(_context);
    }
}
