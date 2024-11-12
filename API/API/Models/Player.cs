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

        public int Bot { get; set; }

        public Player()
        {
            Token = string.Empty;
            Username = string.Empty;
            LastActivity = DateTime.MinValue;
            Friends = new List<string>();
            Requests = new List<Request>();
            Bot = 0;
        }

        public Player(string token, string username, int isBot = 0)
        {
            Token = token;
            Username = username;
            LastActivity = DateTime.UtcNow;
            Friends = new List<string>();
            Requests = new List<Request>();
            Bot = isBot;
        }
    }
}
