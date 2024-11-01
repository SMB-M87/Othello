namespace MVC.Models
{
    public class Game
    {
        public string Token { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Status Status { get; set; }
        public Color PlayersTurn { get; set; }
        public string First { get; set; } = null!;
        public Color FColor { get; set; }
        public string? Second { get; set; } = null!;
        public Color SColor { get; set; }
        public string? Rematch { get; set; } = null!;
        public DateTime Date { get; set; }
        public Color[,] Board { get; set; } = null!;
    }
}
