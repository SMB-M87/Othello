using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Player
    {
        [Key]
        public string Token { get; set; }
        public string Username { get; set; }

        public bool InGame { get; set; }

        public int Won { get; set; }
        public int Lost { get; set; }
        public int Draw { get; set; }

        public Player(string username)
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");
            Username = username;
            InGame = false;
            Won = 0;
            Lost = 0;
            Draw = 0;
        }
    }
}
