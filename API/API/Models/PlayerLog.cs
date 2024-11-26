using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PlayerLog
    {
        [Key]
        public string Token { get; set; }

        [ForeignKey("Player")]
        public string Player { get; set; }

        [Required]
        public string Action { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [Required]
        public string Details { get; set; }

        public PlayerLog()
        {
            Token = string.Empty;
            Player = string.Empty;
            Action = string.Empty;
            Timestamp = DateTime.MinValue;
            Details = string.Empty;
        }

        public PlayerLog(string player, string action, string details)
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");
            Player = player;
            Action = action;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }
}
