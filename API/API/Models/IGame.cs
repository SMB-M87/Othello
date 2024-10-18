namespace API.Models
{
    public enum Color { None = 0, White = 1, Black = 2 };
    public enum Status { Pending = 0, Playing = 1, Finished = 2 };

    public interface IGame
    {
        string Token { get; set; }
        string Description { get; set; }
        Status Status { get; set; }
        Color[,] Board { get; set; }
        Color PlayersTurn { get; set; }
        Color WinningColor();
        bool PossibleMove(int rowMove, int columnMove);
        void MakeMove(int rowMove, int columnMove);
        void Pass();
        bool Finished();
    }
}
