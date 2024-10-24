namespace MVC.Models
{
    public class HomeView
    {
        public string Stats { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;

        public List<string> OnlinePlayers { get; set; } = null!;

        public List<string> OnlineFriends { get; set; } = null!;
        public List<string> OfflineFriends { get; set; } = null!;

        public List<string> FriendRequests { get; set; } = null!;
        public List<string> GameRequests { get; set; } = null!;

        public List<string> SentFriendRequests { get; set; } = null!;
        public List<string> SentGameRequests { get; set; } = null!;
        public List<string> JoinablePlayers { get; set; } = null!;

        public List<GamePending> Games { get; set; } = null!;
    }
}
