namespace MVC.Models
{
    public class RefreshView
    {
        public List<string> OnlinePlayers { get; set; } = null!;
        public List<string> Friends { get; set; } = null!;


        public List<string> FriendRequests { get; set; } = null!;
        public List<string> GameRequests { get; set; } = null!;

        public List<string> SentFriendRequests { get; set; } = null!;
        public List<string> SentGameRequests { get; set; } = null!;

        public List<GamePending> Games { get; set; } = null!;

        public List<string> OnlineFriends => Friends.Intersect(OnlinePlayers).ToList();
        public List<string> OfflineFriends => Friends.Except(OnlinePlayers).ToList();
        public List<string> JoinablePlayers => Games.Select(g => g.Username).ToList();
    }
}
