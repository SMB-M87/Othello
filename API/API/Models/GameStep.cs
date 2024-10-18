namespace API.Models
{
    public class GameStep
    {
        public string Player { get; set; }

        private int y;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    y = value;
                }
            }
        }

        private int x;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    x = value;
                }
            }
        }

        public GameStep(string player, int x = 2, int y = 3)
        {
            Player = player;
            this.X = x;
            this.Y = y;
        }
    }
}
