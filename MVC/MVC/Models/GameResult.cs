﻿namespace MVC.Models
{
    public class GameResult
    {
        public string Token { get; set; } = null!;
        public string Winner { get; set; } = null!;
        public string Loser { get; set; } = null!;
        public string Draw { get; set; } = null!;
    }
}