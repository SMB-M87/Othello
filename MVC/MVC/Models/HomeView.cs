namespace MVC.Models
{
    public class HomeView
    {
        public List<string> OnlinePlayers { get; set; } = null!;
        public List<string> Friends { get; set; } = null!;
        public List<string> SentRequests { get; set; } = null!;
        public List<string> PendingRequests { get; set; } = null!;
    }
}
