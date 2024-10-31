namespace MVC.Models
{
    public class GameOverview
    {
        public string Username { get; set; } = null!;
        public GameResult Result { get; set; } = null!;
        public bool Rematch { get; set; }
    }
}
