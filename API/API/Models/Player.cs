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

        public Player()
        {
            Token = string.Empty;
            Username = string.Empty;
            Friends = new List<string>();
            Requests = new List<Request>();
        }

        public Player(string token, string username)
        {
            Token = token;
            Username = username;
            Friends = new List<string>();
            Requests = new List<Request>();
        }
    }
}
