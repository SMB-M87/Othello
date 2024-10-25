namespace API.Models
{
    public class GamePending
    {
        public string Description { get; set; }
        public string Username { get; set; }
        public string Stats { get; set; }

        public GamePending()
        {
            Description = string.Empty;
            Username = string.Empty;
            Stats = string.Empty;
        }

        public GamePending(string description, string player, string stats)
        {
            Description = description;
            Username = player;
            Stats = stats;
        }
    }
}
