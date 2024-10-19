namespace MVC.Models
{
    public enum Color { None = 0, White = 1, Black = 2 };
    public enum Status { Pending = 0, Playing = 1, Finished = 2 };

    public class Game
    {
        public string Token { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Status Status { get; set; }
        public string First { get; set; } = null!;
        public Color FColor { get; set; }
        public string Second { get; set; } = null!;
        public Color SColor { get; set; }
        public Color[,] Board { get; set; } = null!;
    }
}
