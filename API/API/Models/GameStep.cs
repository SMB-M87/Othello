namespace API.Models
{
    public class GameStep
    {
        public string Token { get; set; }

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

        public GameStep()
        {
            Token = string.Empty;
            Row = 0;
            Column = 0;
        }

        public GameStep(string player, int row = 2, int column = 3)
        {
            Token = player;
            this.Row = row;
            this.Column = column;
        }
    }
}
