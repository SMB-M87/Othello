using API.Models;

namespace API.Data
{
    public interface IUserRepository
    {
        Task<Home> GetView(string token);
        Task<HomePartial> GetPartial(string token);
        Task<HomeProfile> GetProfile(string username, string token);
    }
}
