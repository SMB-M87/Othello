namespace MVC.Models
{
    public class PlayerLog
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Details { get; set; } = null!;
    }
}
