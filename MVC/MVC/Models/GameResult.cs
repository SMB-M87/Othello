namespace MVC.Models
{
    public class GameResult
    {
        public string Winner { get; set; } = null!;
        public string Loser { get; set; } = null!;
        public bool Draw { get; set; }
        public DateTime Date { get; set; }
    }
}
