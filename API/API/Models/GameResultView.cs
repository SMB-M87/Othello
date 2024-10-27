namespace API.Models
{
    public class GameResultView
    {
        public string Winner { get; set; }
        public string Loser { get; set; }
        public bool Draw { get; private set; }
        public Color[,] Board { get; private set; }
        public DateTime Date { get; set; }

        public GameResultView()
        {
            Winner = string.Empty;
            Loser = string.Empty;
            Draw = false;
            Board = new Color[,] { }; 
        }

        public GameResultView(string winner, string loser, bool draw, Color[,] board, DateTime date)
        {
            Winner = winner;
            Loser = loser;
            Draw = draw;
            Board = board;
            Date = date;
        }
    }
}
