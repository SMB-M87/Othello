namespace MVC.Models
{
    public class HomePending
    {
        public string Session { get; set; } = string.Empty;
        public bool InGame { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<GamePending> Games { get; set; } = null!;

        public HomePending()
        {
            InGame = false;
            Games = new List<GamePending>();
        }
    }
}
