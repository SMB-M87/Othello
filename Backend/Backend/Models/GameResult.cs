using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class GameResult
    {
        [Key]
        public string Token { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
        public string Draw { get; set; }

        public GameResult(string token, string winner, string loser, string draw = "Empty")
        {
            Token = token;
            Winner = winner;
            Loser = loser;
            Draw = draw;
        }
    }
}
