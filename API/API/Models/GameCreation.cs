namespace API.Models
{
    public class GameCreation
    {
        public string PlayerToken { get; set; }

        public string Description { get; set; }

        public GameCreation()
        {
            PlayerToken = string.Empty;
            Description = string.Empty;
        }

        public GameCreation(string player_token, string description = "I wanna play a game and don't have any requirements!")
        {
            PlayerToken = player_token;
            Description = description;
        }
    }
}
