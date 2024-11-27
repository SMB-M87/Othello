namespace MVC.Models
{
    public class PlayerView
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;

        public DateTime LastActivity { get; set; }

        public ICollection<string> Friends { get; set; } = null!;
        public ICollection<Request> Requests { get; set; } = null!;
        public int Bot { get; set; }
        public IList<string> Roles { get; set; } = null!;
    }
}
