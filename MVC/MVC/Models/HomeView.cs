﻿namespace MVC.Models
{
    public class HomeView
    {
        public string Stats { get; set; } = null!;
        public List<string> Friends { get; set; } = null!;
        public List<string> Joinable { get; set; } = null!;
        public List<Request> Requests { get; set; } = null!;
        public List<string> SentRequests { get; set; } = null!;
        public List<string> OnlinePlayers { get; set; } = null!;
        public List<string> OnlineFriends { get; set; } = null!;
        public List<Request> JoinRequests { get; set; } = null!;
        public List<GameResult> MatchHistory { get; set; } = null!;
        public List<GameDescription> PendingGames { get; set; } = null!;
    }
}
