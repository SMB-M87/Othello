using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Player
    {
        [Key]
        public string Token { get; set; }
        [Key]
        public string Username { get; set; }

        public DateTime LastActivity { get; set; }

        public ICollection<string> Friends { get; set; }
        public ICollection<string> PendingFriends { get; set; }

        public Player()
        {
            Token = string.Empty;
            Username = string.Empty;
            Friends = new List<string>();
            PendingFriends = new List<string>();
        }

        public Player(string token, string username)
        {
            Token = token;
            Username = username;
            Friends = new List<string>();
            PendingFriends = new List<string>();
        }
    }
}
