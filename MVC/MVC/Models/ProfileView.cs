namespace MVC.Models
{
    public class ProfileView
    {
        public string Username { get; set; } = null!;
        public string Stats { get; set; } = null!;
        public bool IsFriend { get; set; }
        public bool HasSentRequest { get; set; }
        public bool HasPendingRequest { get; set; }
        public List<GameResult> MatchHistory { get; set; } = null!;
    }
}
