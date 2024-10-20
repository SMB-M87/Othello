namespace MVC.Models
{
    public class Player
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;

        public DateTime LastActivity { get; set; }

        public ICollection<string> Friends { get; set; } = null!;
        public ICollection<string> PendingFriends { get; set; } = null!;
    }
}
