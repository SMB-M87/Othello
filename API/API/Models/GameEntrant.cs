﻿namespace API.Models
{
    public class GameEntrant
    {
        public string Token { get; set; }
        public string Player { get; set; }

        public GameEntrant()
        {
            Token = string.Empty;
            Player = string.Empty;
        }

        public GameEntrant(string token, string player)
        {
            Token = token;
            Player = player;
        }
    }
}
