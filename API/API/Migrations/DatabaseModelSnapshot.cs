using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Game", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FColor")
                        .HasColumnType("int");

                    b.Property<string>("First")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PlayersTurn")
                        .HasColumnType("int");

                    b.Property<string>("Rematch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SColor")
                        .HasColumnType("int");

                    b.Property<string>("Second")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.HasIndex("First");

                    b.HasIndex("Rematch");

                    b.HasIndex("Second");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("API.Models.GameResult", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Draw")
                        .HasColumnType("bit");

                    b.Property<bool>("Forfeit")
                        .HasColumnType("bit");

                    b.Property<string>("Loser")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Token");

                    b.HasIndex("Loser");

                    b.HasIndex("Winner");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("API.Models.Player", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Bot")
                        .HasColumnType("int");

                    b.Property<string>("Friends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("Requests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Token");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Token = "karen",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5608),
                            Requests = "[]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5613),
                            Requests = "[]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5614),
                            Requests = "[]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5615),
                            Requests = "[]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5616),
                            Requests = "[]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5617),
                            Requests = "[]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5618),
                            Requests = "[]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5618),
                            Requests = "[]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "briar",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5620),
                            Requests = "[]",
                            Username = "Briar"
                        },
                        new
                        {
                            Token = "lambert",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5621),
                            Requests = "[]",
                            Username = "Lambert"
                        },
                        new
                        {
                            Token = "identity",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5622),
                            Requests = "[]",
                            Username = "Identity"
                        },
                        new
                        {
                            Token = "salie",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5623),
                            Requests = "[]",
                            Username = "Salie"
                        },
                        new
                        {
                            Token = "pipo",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5623),
                            Requests = "[]",
                            Username = "Pipo"
                        },
                        new
                        {
                            Token = "gissa",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5624),
                            Requests = "[]",
                            Username = "Gissa"
                        },
                        new
                        {
                            Token = "hidde",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5626),
                            Requests = "[]",
                            Username = "Hidde"
                        },
                        new
                        {
                            Token = "noga",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5626),
                            Requests = "[]",
                            Username = "Noga"
                        },
                        new
                        {
                            Token = "nastrovia",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5724),
                            Requests = "[]",
                            Username = "Nastrovia"
                        },
                        new
                        {
                            Token = "pedro",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5726),
                            Requests = "[]",
                            Username = "Pedro"
                        },
                        new
                        {
                            Token = "ahmed",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5727),
                            Requests = "[]",
                            Username = "Ahmed"
                        },
                        new
                        {
                            Token = "nadege",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5728),
                            Requests = "[]",
                            Username = "Nadege"
                        },
                        new
                        {
                            Token = "rachel",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5728),
                            Requests = "[]",
                            Username = "Rachel"
                        },
                        new
                        {
                            Token = "ff20c418-f1b0-4f16-b582-294be25c24ef",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5729),
                            Requests = "[]",
                            Username = "mediator"
                        },
                        new
                        {
                            Token = "58a479fd-ae6f-4474-a147-68cbdb62c19b",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5730),
                            Requests = "[]",
                            Username = "admin"
                        },
                        new
                        {
                            Token = "deleted",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5731),
                            Requests = "[]",
                            Username = "Deleted"
                        });
                });

            modelBuilder.Entity("API.Models.Game", b =>
                {
                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("First")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("Rematch")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("Second")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("API.Models.GameResult", b =>
                {
                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("Loser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("Winner")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
        }
    }
}
