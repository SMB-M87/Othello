namespace API.Models
{
    public class PlayerActivity
    {
        public string Token { get; set; }

        public PlayerActivity()
        {
            Token = string.Empty;
        }

        public PlayerActivity(string token)
        {
            Token = token;
        }
    }
}
