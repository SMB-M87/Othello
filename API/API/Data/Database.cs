using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

                entity.Property(e => e.Token)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.Property(e => e.Status)
                      .IsRequired();

                entity.Property(e => e.PlayersTurn)
                      .IsRequired();

                entity.HasOne<Player>()
                      .WithMany()
                      .HasForeignKey(e => e.First)
                      .HasPrincipalKey(p => p.Token)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.First)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.Property(e => e.FColor)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.HasOne<Player>()
                      .WithMany()
                      .HasForeignKey(e => e.Second)
                      .HasPrincipalKey(p => p.Token)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.SColor)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.Property(e => e.Date)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

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

                entity.Property(e => e.Token)
                      .IsRequired();

                entity.HasIndex(e => e.Username)
                      .IsUnique();

                entity.Property(e => e.Username)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.Property(e => e.LastActivity);

                entity.Property(e => e.Friends)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<ICollection<string>>(v, new JsonSerializerOptions()) ?? new List<string>()
                    )
                    .HasColumnType("nvarchar(max)")
                    .Metadata.SetValueComparer(new StringCollectionComparer());

                entity.Property(e => e.Requests)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<ICollection<Request>>(v, new JsonSerializerOptions()) ?? new List<Request>()
                    )
                    .HasColumnType("nvarchar(max)")
                    .Metadata.SetValueComparer(new RequestCollectionComparer());
            });

            builder.Entity<GameResult>(entity =>
            {
                entity.HasKey(e => e.Token);

                entity.Property(e => e.Token)
                      .IsRequired();

                entity.HasOne<Player>()
                      .WithMany()
                      .HasForeignKey(e => e.Winner)
                      .HasPrincipalKey(p => p.Token)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Player>()
                      .WithMany()
                      .HasForeignKey(e => e.Loser)
                      .HasPrincipalKey(p => p.Token)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Draw)
                      .IsRequired()
                      .ValueGeneratedNever()
                      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity.Property(e => e.Board)
                      .IsRequired()
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, new JsonSerializerOptions { Converters = { new ColorArrayConverter() } }),
                          v => JsonSerializer.Deserialize<Color[,]>(v, new JsonSerializerOptions { Converters = { new ColorArrayConverter() } }) ?? new Color[8, 8]
                      )
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.Date)
                      .IsRequired();
            });

            new DbInitializer(builder).Seed();
        }
    }
}