namespace MVC.Models
{
    public class Home
    {
        public string Stats { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;

        public HomePartial Partial { get; set; } = null!;

        public Home()
        {
            Stats = string.Empty;
            MatchHistory = new List<GameResult>();
            Partial = new();
        }
    }
}
