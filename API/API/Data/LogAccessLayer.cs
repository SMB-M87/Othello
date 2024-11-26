using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LogAccessLayer : ILogRepository
    {
        private readonly Database _context;

        public LogAccessLayer(Database context)
        {
            _context = context;
        }

        public async Task Create(PlayerLog log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<PlayerLog?> Get(string token)
        {
            var response = await _context.Logs.AsNoTracking().FirstOrDefaultAsync(s => s.Token == token);

            if (response is not null)
            {
                return response;
            }
            return null;
        }

        public async Task<List<PlayerLog>> GetLogs(string token)
        {
            if (token == "null")
                return await _context.Logs.AsNoTracking().OrderByDescending(p => p.Timestamp).ToListAsync();
            else
                return await _context.Logs.AsNoTracking().Where(l => l.Username == token || l.Token == token).OrderByDescending(p => p.Timestamp).ToListAsync();
        }
    }
}
