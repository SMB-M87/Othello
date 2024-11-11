using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Game : IGame
    {
        private const int boardScope = 8;
        private readonly int[,] direction = new int[8, 2] {
                                {  0,  1 },         // right
                                {  0, -1 },         // left
                                {  1,  0 },         // bottom
                                { -1,  0 },         // top
                                {  1,  1 },         // bottom right
                                {  1, -1 },         // bottom left
                                { -1,  1 },         // top right
                                { -1, -1 } };       // top left

        [Key]
        public string Token { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public Color PlayersTurn { get; private set; }

        [ForeignKey("Player")]
        public string First { get; private set; }
        public Color FColor { get; private set; }

        [ForeignKey("Player")]
        public string? Second { get; private set; }
        public Color SColor { get; private set; }

        [ForeignKey("Player")]
        public string? Rematch { get; private set; }
        public DateTime Date { get; private set; }

        private Color[,] board;
        public Color[,] Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
            }
        }

        public Game()
        {
            Token = string.Empty;
            Description = string.Empty;
            First = string.Empty;
            FColor = Color.None;
            Second = null;
            SColor = Color.None;
            Rematch = null;
            Date = DateTime.MinValue;
            board = new Color[boardScope, boardScope];
        }

        public Game(string first, string description = "I wanna play a game and don't have any requirements!", string rematch = "")
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "q").Replace("+", "r");

            board = new Color[boardScope, boardScope];
            Board[3, 3] = Color.White;
            Board[4, 4] = Color.White;
            Board[3, 4] = Color.Black;
            Board[4, 3] = Color.Black;

            PlayersTurn = Color.Black;
            Description = description;
            Status = Status.Pending;

            First = first;
            FColor = new Random().Next(1, 3) == 1 ? Color.White : Color.Black;

            Second = null;
            SColor = GetOpponentsColor(FColor);

            if (string.IsNullOrEmpty(rematch))
                Rematch = null;
            else
                Rematch = rematch;

            for (int row = 0; row < boardScope; row++)
            {
                for (int column = 0; column < boardScope; column++)
                {
                    if (PossibleMove(row, column, PlayersTurn))
                    {
                        Board[row, column] = Color.PossibleMove;
                    }
                }
            }
            Date = DateTime.UtcNow;
        }

        public Game(string token, string first, Color fcolor, string? second = null, Status game = Status.Pending, Color turn = Color.Black, string description = "I wanna play a game and don't have any requirements!")
        {
            Token = token;

            board = new Color[boardScope, boardScope];
            Board[3, 3] = Color.White;
            Board[4, 4] = Color.White;
            Board[3, 4] = Color.Black;
            Board[4, 3] = Color.Black;

            PlayersTurn = turn;
            Description = description;
            Status = game;

            First = first;
            FColor = fcolor;

            Second = second;
            SColor = GetOpponentsColor(FColor);

            Date = DateTime.UtcNow;
        }

        public void SetSecondPlayer(string secondPlayerToken)
        {
            if (Second == null)
            {
                Second = secondPlayerToken;
                Date = DateTime.UtcNow;
                SetPlayingStatus();
            }
            else
            {
                throw new InvalidGameOperationException("The second player is already set and cannot be changed.");
            }
        }

        public bool IsThereAPossibleMove(Color color)
        {
            if (color == Color.None)
                throw new InvalidGameOperationException("None is not a valid color!");

            for (int rowMove = 0; rowMove < boardScope; rowMove++)
            {
                for (int columnMove = 0; columnMove < boardScope; columnMove++)
                {
                    if (PossibleMove(rowMove, columnMove, color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMove(int rowMove, int columnMove)
        {
            if (!PositionInbetweenBoardLimits(rowMove, columnMove))
                throw new InvalidGameOperationException($"Move ({rowMove},{columnMove}) is outside the board!");

            return PossibleMove(rowMove, columnMove, PlayersTurn);
        }

        public bool Finished()
        {
            return !(IsThereAPossibleMove(PlayersTurn) && IsThereAPossibleMove(GetOpponentsColor(PlayersTurn)));
        }

        public void Pass()
        {
            DateTime now = DateTime.UtcNow;
            DateTime end = Date.AddSeconds(30);
            double remainingSeconds = (end - now).TotalSeconds;

            if (Math.Floor(remainingSeconds) > 0 && IsThereAPossibleMove(PlayersTurn))
            {
                throw new InvalidGameOperationException("You're not allowed to pass, you can still make a move!");
            }
            else
            {
                ChangeTurns();

                for (int row = 0; row < boardScope; row++)
                {
                    for (int column = 0; column < boardScope; column++)
                    {
                        if (Board[row, column] == Color.PossibleMove)
                        {
                            Board[row, column] = Color.None;
                        }
                        if (PossibleMove(row, column, PlayersTurn))
                        {
                            Board[row, column] = Color.PossibleMove;
                        }
                    }
                }
            }
        }

        public void MakeMove(int rowMove, int columnMove)
        {
            if (Status == Status.Playing && PossibleMove(rowMove, columnMove))
            {
                for (int i = 0; i < 8; i++)
                {
                    FlipOpponentsPawnsInSpecifiedDirectionIfEnclosed(rowMove, columnMove, PlayersTurn, direction[i, 0], direction[i, 1]);
                }
                Board[rowMove, columnMove] = PlayersTurn;
                ChangeTurns();

                for (int row = 0; row < boardScope; row++)
                {
                    for (int column = 0; column < boardScope; column++)
                    {
                        if (Board[row, column] == Color.PossibleMove)
                        {
                            Board[row, column] = Color.None;
                        }
                        if (PossibleMove(row, column, PlayersTurn))
                        {
                            Board[row, column] = Color.PossibleMove;
                        }
                    }
                }
                Date = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidGameOperationException($"Move ({rowMove},{columnMove}) is not possible!");
            }
        }

        public Color WinningColor()
        {
            int numberWhite = 0;
            int numberBlack = 0;
            for (int rowMove = 0; rowMove < boardScope; rowMove++)
            {
                for (int columnMove = 0; columnMove < boardScope; columnMove++)
                {
                    if (board[rowMove, columnMove] == Color.White)
                        numberWhite++;
                    else if (board[rowMove, columnMove] == Color.Black)
                        numberBlack++;
                }
            }
            if (numberWhite > numberBlack)
                return Color.White;
            if (numberBlack > numberWhite)
                return Color.Black;
            return Color.None;
        }

        public void Finish()
        {
            for (int row = 0; row < boardScope; row++)
            {
                for (int column = 0; column < boardScope; column++)
                {
                    if (Board[row, column] == Color.PossibleMove)
                    {
                        Board[row, column] = Color.None;
                    }
                }
            }

            Status = Status.Finished;
            PlayersTurn = Color.None;
        }

        private void SetPlayingStatus()
        {
            Status = Status.Playing;
        }

        private static Color GetOpponentsColor(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else if (color == Color.Black)
                return Color.White;
            else
                return Color.None;
        }

        private bool PossibleMove(int rowMove, int columnMove, Color color)
        {
            for (int i = 0; i < 8; i++)
            {
                {
                    if (PawnToEncloseInSpecifiedDirection(rowMove, columnMove, color, direction[i, 0], direction[i, 1]))
                        return true;
                }
            }
            return false;
        }

        private void ChangeTurns()
        {
            if (PlayersTurn == Color.White)
                PlayersTurn = Color.Black;
            else if (PlayersTurn == Color.Black)
                PlayersTurn = Color.White;
            Date = DateTime.UtcNow;
        }

        private static bool PositionInbetweenBoardLimits(int row, int column)
        {
            return row >= 0 && row < boardScope && column >= 0 && column < boardScope;
        }

        private bool IsPlaceOnBoardFree(int rowMove, int columnMove)
        {
            return PositionInbetweenBoardLimits(rowMove, columnMove) && (Board[rowMove, columnMove] == Color.None || Board[rowMove, columnMove] == Color.PossibleMove);
        }

        private bool PawnToEncloseInSpecifiedDirection(int rowMove, int columnMove, Color colorPlayer, int rowDirection, int columnDirection)
        {
            int row, column;
            Color colorOpponent = GetOpponentsColor(colorPlayer);

            if (!IsPlaceOnBoardFree(rowMove, columnMove))
                return false;

            row = rowMove + rowDirection;
            column = columnMove + columnDirection;

            int NumberOfAdjacentPawnsOfOpponent = 0;

            while (PositionInbetweenBoardLimits(row, column) && Board[row, column] == colorOpponent)
            {
                row += rowDirection;
                column += columnDirection;
                NumberOfAdjacentPawnsOfOpponent++;
            }
            return PositionInbetweenBoardLimits(row, column) && Board[row, column] == colorPlayer && NumberOfAdjacentPawnsOfOpponent > 0;
        }

        private bool FlipOpponentsPawnsInSpecifiedDirectionIfEnclosed(int rowMove, int columnMove, Color colorPlayer, int rowDirection, int columnDirection)
        {
            int row, column;
            Color colorOpponent = GetOpponentsColor(colorPlayer);
            bool pawnFlipped = false;

            if (PawnToEncloseInSpecifiedDirection(rowMove, columnMove, colorPlayer, rowDirection, columnDirection))
            {
                row = rowMove + rowDirection;
                column = columnMove + columnDirection;

                while (Board[row, column] == colorOpponent)
                {
                    Board[row, column] = colorPlayer;
                    row += rowDirection;
                    column += columnDirection;
                }
                pawnFlipped = true;
            }
            return pawnFlipped;
        }
    }
}
