using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PlayerLog
    {
        [Key]
        public string Token { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Action { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public string Details { get; set; }

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
