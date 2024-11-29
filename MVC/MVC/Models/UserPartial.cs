namespace MVC.Models
{
    public class UserPartial
    {
        public UserPending Pending { get; set; } = null!;

        public List<string> OnlinePlayers { get; set; } = null!;
        public List<string> PlayersInGame { get; set; } = null!;
        public List<string> Friends { get; set; } = null!;

        public List<string> FriendRequests { get; set; } = null!;
        public List<string> GameRequests { get; set; } = null!;

        public List<string> SentFriendRequests { get; set; } = null!;
        public List<string> SentGameRequests { get; set; } = null!;

        public List<string> OnlineFriends => Friends.Intersect(OnlinePlayers).ToList();
        public List<string> Online => OnlinePlayers.Except(OnlineFriends).ToList();
        public List<string> OfflineFriends => Friends.Except(OnlinePlayers).ToList();
        public List<string> JoinablePlayers => Pending.Games.Select(g => g.Username).ToList();
        public List<string> InvitablePlayers => OnlinePlayers.Except(Pending.Games.Select(g => g.Username)).Except(PlayersInGame).ToList();

        public UserPartial()
        {
            Pending = new();
            OnlinePlayers = new List<string>();
            PlayersInGame = new List<string>();
            Friends = new List<string>();
            FriendRequests = new List<string>();
            GameRequests = new List<string>();
            SentFriendRequests = new List<string>();
            SentGameRequests = new List<string>();
        }
    }
}
