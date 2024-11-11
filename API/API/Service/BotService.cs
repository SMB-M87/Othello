using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class BotService
    {
        private readonly Database _context;

        public BotService(Database context)
        {
            _context = context;
        }

        public async Task AcceptFriendRequestsAsync()
        {
            var bots = await _context.Players.Where(p => p.IsBot).ToListAsync();

            foreach (var bot in bots)
            {
                bot.LastActivity = DateTime.UtcNow;
                _context.Entry(bot).Property(p => p.LastActivity).IsModified = true;

                var pendingRequests = bot.Requests
                    .Where(r => r.Type == Inquiry.Friend && !bot.Friends.Contains(r.Username))
                    .ToList();

                foreach (var request in pendingRequests)
                {                    
                    var requester = await _context.Players.FirstOrDefaultAsync(p => p.Username == request.Username);
                    
                    if (requester != null)
                    {
                        bot.Friends.Add(request.Username);
                        bot.Requests.Remove(request);
                        _context.Entry(bot).Property(p => p.Friends).IsModified = true;
                        _context.Entry(bot).Property(p => p.Requests).IsModified = true;

                        requester.Friends.Add(bot.Username);
                        _context.Entry(requester).Property(p => p.Friends).IsModified = true;
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
