namespace MVC.Models
{
    public class PlayerLog
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Details { get; set; } = null!;

        public PlayerLog()
        {
            Token = string.Empty;
            Username = string.Empty;
            Action = string.Empty;
            Timestamp = DateTime.MinValue;
            Details = string.Empty;
        }

        public PlayerLog(string player, string action, string details)
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");
            Username = player;
            Action = action;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }
}
