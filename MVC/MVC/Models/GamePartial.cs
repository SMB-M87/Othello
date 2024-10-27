namespace MVC.Models
{
    public class GamePartial
    {
        public bool InGame { get; set; }
        public Color PlayersTurn { get; set; }
        public Color[,] Board { get; set; } = null!;
    }
}
