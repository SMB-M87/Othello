using API.Models;

namespace API.Data
{
    public interface IHomeRepository
    {
        Home GetView(string token);
        HomePartial GetPartial(string token);
        HomeProfile GetProfile(string username, string token);
    }
}
