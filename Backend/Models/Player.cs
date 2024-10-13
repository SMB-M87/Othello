using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Player
    {
        [Key]
        public string Token { get; set; }
        [Required]
        public string Username { get; set; }

        public List<string> Friends { get; set; }
        public List<string> PendingFriends { get; set; }

        public Player()
        {
            Token = string.Empty;
            Username = string.Empty;
            Friends = new List<string>();
            PendingFriends = new List<string>();
        }

        public Player(string username)
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");
            Username = username;
            Friends = new List<string>();
            PendingFriends = new List<string>();
        }
    }
}
