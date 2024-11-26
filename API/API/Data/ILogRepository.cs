using API.Models;

namespace API.Data
{
    public interface ILogRepository
    {
        Task Create(PlayerLog log);
        Task<PlayerLog?> Get(string token);
        Task<List<PlayerLog>> GetLogs(string token);
    }
}
