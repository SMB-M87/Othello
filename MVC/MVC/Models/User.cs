namespace MVC.Models
{
    public class User
    {
        public string Stats { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;

        public UserPartial Partial { get; set; } = null!;

        public User()
        {
            Stats = string.Empty;
            MatchHistory = new List<GameResult>();
            Partial = new();
        }
    }
}
