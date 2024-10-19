namespace MVC.Models
{
    public class ProfileView
    {
        public bool IsFriend { get; set; }
        public bool HasSentRequest { get; set; }
        public bool HasPendingRequest { get; set; }
        public string Stats { get; set; } = null!;
        public string Username { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;
    }
}
