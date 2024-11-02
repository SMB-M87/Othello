namespace API.Models
{
    public class GamePlay
    {
        public string Opponent { get; set; } = null!;
        public Color Color { get; set; }
        public GamePartial Partial { get; set; } = null!;
    }
}
