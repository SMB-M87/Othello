namespace MVC.Models
{
    public class GameView
    {
        public string Opponent { get; set; } = null!;
        public Color Color { get; set; }
        public Color PlayersTurn { get; set; }
        public Color[,] Board { get; set; } = null!;
    }
}
