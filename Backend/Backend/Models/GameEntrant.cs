namespace Backend.Models
{
    public class GameEntrant
    {
        public string Token { get; set; }
        public Player Player { get; set; }

        public GameEntrant(string token, Player player)
        {
            Token = token;
            Player = player;
        }
    }
}
