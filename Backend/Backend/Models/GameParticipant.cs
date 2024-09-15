using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class GameParticipant
    {
        [Key]
        public string Token { get; set; }
        public Color Color { get; set; }

        public GameParticipant(string token)
        {
            Token = token;
            Color = Color.None;
        }
    }
}
