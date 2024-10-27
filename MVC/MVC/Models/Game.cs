namespace MVC.Models
{
    public class Game
    {
        public string Opponent { get; set; } = null!;
        public Color Color { get; set; }
        public GamePartial Partial { get; set; } = null!;
    }
}
