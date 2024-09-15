namespace Backend.Models
{
    public class GameCreation
    {
        public Player Player { get; set; }

        public string Description { get; set; }

        public GameCreation(Player player, string description)
        {
            Player = player;
            Description = description;
        }
    }
}
