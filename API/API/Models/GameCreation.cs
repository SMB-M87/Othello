namespace API.Models
{
    public class GameCreation
    {
        public string PlayerToken { get; set; }
        public string Description { get; set; }
        public string? Rematch { get; set; }

        public GameCreation()
        {
            PlayerToken = string.Empty;
            Description = string.Empty;
            Rematch = null;
        }

        public GameCreation(string player_token, string description = "I wanna play a game and don't have any requirements!", string rematch = "")
        {
            PlayerToken = player_token;
            Description = description;

            if (string.IsNullOrEmpty(rematch))
                Rematch = null;
            else
                Rematch = rematch;
        }
    }
}
