/*using MVC.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

*//*
 * Add-Migration Initial -Context Database
 * Update-Database -Context MVC
*//*
namespace MVC.Data
{
    public class Database : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;

        public Database(DbContextOptions<Database> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Token);
                entity.Property(e => e.Username).IsRequired();

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

            new DbInitializer(builder).Seed();
        }

    }
}
*/