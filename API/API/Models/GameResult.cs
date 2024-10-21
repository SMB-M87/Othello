using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class GameResult
    {
        [Key]
        public string Token { get; private set; }
        [ForeignKey("Player")]
        public string Winner { get; set; }
        [ForeignKey("Player")]
        public string Loser { get; set; }
        [Required]
        public bool Draw { get; private set; }
        public DateTime Date { get; set; }

        public GameResult()
        {
            Token = string.Empty;
            Winner = string.Empty;
            Loser = string.Empty;
            Draw = false;
        }

        public GameResult(string token, string winner, string loser, bool draw = false)
        {
            Token = token;
            Winner = winner;
            Loser = loser;
            Draw = draw;
            Date = DateTime.UtcNow;
        }
    }
}
