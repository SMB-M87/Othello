namespace Backend.Models
{
    public class GameCreation
    {
        public string Player { get; set; }

        public string Description { get; set; }

        public GameCreation(string player, string description = "I wanna play a game and don't have any requirements!")
        {
            Player = player;
            Description = description;
        }
    }
}
