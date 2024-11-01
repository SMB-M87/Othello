namespace MVC.Models
{
    public enum Inquiry { Friend = 0, Game = 1, None = 2 }

    public class Request
    {
        public Inquiry Type { get; set; }
        public string Username { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
