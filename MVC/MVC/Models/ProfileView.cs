namespace MVC.Models
{
    public class ProfileView
    {
        public string Stats { get; set; } = null!;
        public string Username { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;
    }
}
