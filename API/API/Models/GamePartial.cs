﻿namespace API.Models
{
    public class GamePartial
    {
        public bool InGame { get; set; }
        public Color PlayersTurn { get; set; }
        public Color[,] Board { get; set; } = null!;
        public string Time { get; set; } = null!;
    }
}
