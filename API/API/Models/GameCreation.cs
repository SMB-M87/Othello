namespace API.Models
{
    public class GameCreation
    {
        public string Player { get; set; }

        public string Description { get; set; }

        public GameCreation()
        {
            Player = string.Empty;
            Description = string.Empty;
        }

        public GameCreation(string player, string description = "I wanna play a game and don't have any requirements!")
        {
            Player = player;
            Description = description;
        }
    }
}
