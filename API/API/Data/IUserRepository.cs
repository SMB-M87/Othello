using API.Models;

namespace API.Data
{
    public interface IUserRepository
    {
        Task<User> GetView(string token);
        Task<UserPartial> GetPartial(string token);
        Task<HomeProfile> GetProfile(string username, string token);
    }
}
