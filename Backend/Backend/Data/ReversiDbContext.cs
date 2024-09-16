/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Backend.Models;
using System.Collections;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Backend.Data
{
    public class ReversiDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameResult> Results { get; set; }

        public ReversiDbContext(DbContextOptions<ReversiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //var GamePlayerToString = new GamePlayerConversion();
            //var BoardArrayToString = new BoardConversion();

            builder
                .Entity<Game>()
                .Property(e => e.First)
                .HasConversion<GamePlayerConversion>();

            builder
                .Entity<Game>()
                .Property(e => e.Second)
                .HasConversion<GamePlayerConversion>();

            builder
                .Entity<Game>()
                .Property(e => e.Board)
                .HasConversion<BoardConversion>()
                .Metadata
                .SetValueComparer(new ValueComparer<Color[,]>(
                    (a, b) => StructuralComparisons.StructuralEqualityComparer.Equals(a, b),
                    a => a.GetHashCode())); ;

            base.OnModelCreating(builder);
            new ReversiDbInitializer(builder).Seed();
        }
    }

    public class GamePlayerConversion : ValueConverter<GamePlayer, string>
    {
        public GamePlayerConversion() : base(p => GamePlayerToString(p), s => StringToGamePlayer(s))
        {

        }

        public static string GamePlayerToString(GamePlayer value)
        {
            if (value == null)
            {
                return null;
            }

            return (int)value.Color + " " + value.Token;
        }

        public static GamePlayer StringToGamePlayer(string value)
        {
            if (value == null || value == string.Empty)
            {
                return null;
            }

            string[] array = value.Split(' ');
            GamePlayer result = new(array[1]);
            result.Color = (Color)int.Parse(array[0]);

            return result;
        }
    }

    public class BoardConversion : ValueConverter<Color[,], string>
    {
        public BoardConversion() : base(b => ArrayToString(b), s => StringToArray(s))
        {

        }

        public static string ArrayToString(Color[,] value)
        {
            if (value == null)
            {
                return null;
            }

            string result = string.Empty;
            int row = 0;
            int column = 0;

            foreach (Color c in value)
            {
                if (column < 8)
                {
                    result += (int)c;
                    column++;

                    if (column == 8)
                    {
                        result += " \n";
                        row++;
                        column = 0;
                    }
                }
            }
            return result;
        }

        public static Color[,] StringToArray(string value)
        {
            if (value == null || value == string.Empty)
            {
                return null;
            }

            Color[,] result = new Color[8, 8];
            int row = 0;
            int column = 0;

            foreach (char c in value)
            {
                if (char.IsNumber(c))
                {
                    result[row, column] = (Color)Char.GetNumericValue(c);
                    column++;

                    if (column == 8)
                    {
                        row++;
                        column = 0;
                    }
                }
            }
            return result;
        }
    }
}
*/