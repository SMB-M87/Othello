namespace Backend.Models
{
    public class GameCreation
    {
        public Player Player { get; set; }

        public string Description { get; set; }

        public GameCreation(Player player, string description = "I wanna play a game and don't have any requirements!")
        {
            Player = player;
            Description = description;
        }
    }
}
