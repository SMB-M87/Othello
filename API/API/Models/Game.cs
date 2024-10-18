using System.ComponentModel.DataAnnotations;

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
        public string Token { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Color PlayersTurn { get; set; }

        public string First { get; set; }
        public Color FColor { get; set; }

        public string Second { get; set; }
        public Color SColor { get; set; }

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
            Second = string.Empty;
            SColor = Color.None;
            board = new Color[boardScope, boardScope];
        }

        public Game(string first, string description = "I wanna play a game and don't have any requirements!")
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

            Second = string.Empty;
            SColor = GetOpponentsColor(FColor);
        }

        public void Pass()
        {
            if (IsThereAPossibleMove(PlayersTurn))
            {
                throw new InvalidGameOperationException("You're not allowed to pass, you can still make a move!");
            }
            else
            {
                ChangeTurns();
            }
        }

        public bool Finished()
        {
            return !(IsThereAPossibleMove(PlayersTurn) && IsThereAPossibleMove(GetOpponentsColor(PlayersTurn)));
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

        public bool PossibleMove(int rowMove, int columnMove)
        {
            if (!PositionInbetweenBoardLimits(rowMove, columnMove))
                throw new InvalidGameOperationException($"Move ({rowMove},{columnMove}) is outside the board!");

            return PossibleMove(rowMove, columnMove, PlayersTurn);
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
            }
            else
            {
                throw new InvalidGameOperationException($"Move ({rowMove},{columnMove}) is not possible!");
            }
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

        private bool IsThereAPossibleMove(Color color)
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
        }

        private static bool PositionInbetweenBoardLimits(int row, int column)
        {
            return row >= 0 && row < boardScope && column >= 0 && column < boardScope;
        }

        private bool IsPlaceOnBoardFree(int rowMove, int columnMove)
        {
            return PositionInbetweenBoardLimits(rowMove, columnMove) && Board[rowMove, columnMove] == Color.None;
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
