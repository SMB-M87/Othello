namespace Backend.Models
{
    public class GameStep
    {
        public GameParticipant Participant { get; set; }

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

        public GameStep(GameParticipant participant, int x, int y)
        {
            Participant = participant;
            this.X = x;
            this.Y = y;
        }
    }
}
