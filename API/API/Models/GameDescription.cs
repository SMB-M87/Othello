namespace API.Models
{
    public class GameDescription
    {
        public string Description { get; set; }
        public string Player { get; set; }
        public string Stats { get; set; }

        public GameDescription()
        {
            Description = string.Empty;
            Player = string.Empty;
            Stats = string.Empty;
        }

        public GameDescription(string description, string player, string stats)
        {
            Description = description;
            Player = player;
            Stats = stats;
        }
    }
}
