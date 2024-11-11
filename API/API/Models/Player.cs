using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Player
    {
        [Key]
        public string Token { get; set; }
        [Required]
        public string Username { get; private set; }

        public DateTime LastActivity { get; set; }

        public ICollection<string> Friends { get; set; }
        public ICollection<Request> Requests { get; set; }

        public bool IsBot { get; set; }

        public Player()
        {
            Token = string.Empty;
            Username = string.Empty;
            LastActivity = DateTime.MinValue;
            Friends = new List<string>();
            Requests = new List<Request>();
            IsBot = false;
        }

        public Player(string token, string username, bool isBot = false)
        {
            Token = token;
            Username = username;
            LastActivity = DateTime.UtcNow;
            Friends = new List<string>();
            Requests = new List<Request>();
            IsBot = isBot;
        }
    }
}
