namespace API.Models
{
    public enum Color { None = 0, White = 1, Black = 2, PossibleMove = 3 };
    public enum Status { Pending = 0, Playing = 1, Finished = 2 };

    public interface IGame
    {
        string Token { get; }
        string Description { get; }
        Status Status { get; }
        Color[,] Board { get; set; }
        Color PlayersTurn { get; }
        void SetSecondPlayer(string secondPlayerToken);
        bool IsThereAPossibleMove(Color color);
        bool PossibleMove(int rowMove, int columnMove);
        bool Finished();
        void Pass();
        void ByPass();
        void MakeMove(int rowMove, int columnMove);
        Color WinningColor();
        void Finish();
    }
}
