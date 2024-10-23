namespace API.Models
{
    public class GameMove
    {
        public string PlayerToken { get; set; }

        private int _column;
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _column = value;
                }
            }
        }

        private int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _row = value;
                }
            }
        }

        public GameMove()
        {
            PlayerToken = string.Empty;
            Row = 0;
            Column = 0;
        }

        public GameMove(string player_token, int row = 2, int column = 3)
        {
            PlayerToken = player_token;
            this.Row = row;
            this.Column = column;
        }
    }
}
