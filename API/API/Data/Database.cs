using API.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

/*
 * Package Manager Console:
 * Add-Migration NameOfMigration -Verbose
 * Update-Database -Verbose
 * 
 * Add-Migration AddLastActivity
 * Update-Database
*/
namespace API.Data
{
    public class Database : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameResult> Results { get; set; }

        public Database(DbContextOptions<Database> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Token);
                entity.Property(e => e.Description);
                entity.Property(e => e.Status);
                entity.Property(e => e.PlayersTurn);
                entity.Property(e => e.First);
                entity.Property(e => e.FColor);
                entity.Property(e => e.Second);
                entity.Property(e => e.SColor);

                entity.Property(e => e.Board)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions { Converters = { new ColorArrayConverter() } }),
                        v => JsonSerializer.Deserialize<Color[,]>(v, new JsonSerializerOptions { Converters = { new ColorArrayConverter() } }) ?? new Color[8, 8]
                    )
                    .HasColumnType("nvarchar(max)");
            });

            builder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Token);
                entity.HasKey(e => e.Username);

                entity.Property(e => e.LastActivity);

                entity.Property(e => e.Friends)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<ICollection<string>>(v, new JsonSerializerOptions()) ?? new List<string>()
                    )
                    .HasColumnType("nvarchar(max)")
                    .Metadata.SetValueComparer(new StringCollectionComparer());

                entity.Property(e => e.PendingFriends)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions()) ?? new List<string>()
                    )
                    .HasColumnType("nvarchar(max)")
                    .Metadata.SetValueComparer(new StringCollectionComparer());
            });

            builder.Entity<GameResult>(entity =>
            {
                entity.HasKey(e => e.Token);
                entity.Property(e => e.Winner);
                entity.Property(e => e.Loser);
                entity.Property(e => e.Draw);
            });

            new DbInitializer(builder).Seed();
        }
    }
}