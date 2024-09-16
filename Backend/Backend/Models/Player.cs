using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Player
    {
        [Key]
        public string Token { get; set; }
        public string Username { get; set; }

        public List<string> Friends;
        public List<string> PendingFriends;

        public Player(string username)
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");
            Username = username;
            Friends = new List<string>();
            PendingFriends = new List<string>();
        }
    }
}
