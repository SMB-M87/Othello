namespace API.Models
{
    public class GameResultView
    {
        public string Winner { get; set; }
        public string Loser { get; set; }
        public bool Draw { get; private set; }
        public DateTime Date { get; set; }

        public GameResultView()
        {
            Winner = string.Empty;
            Loser = string.Empty;
            Draw = false;
        }

        public GameResultView(string winner, string loser, bool draw, DateTime date)
        {
            Winner = winner;
            Loser = loser;
            Draw = draw;
            Date = date;
        }
    }
}
