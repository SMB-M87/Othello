﻿using API.Data;
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
                            Token = "mary",
                            Bot = 2,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4429),
                            Requests = "[]",
                            Username = "Mary"
                        },
                        new
                        {
                            Token = "user-987456456198135",
                            Bot = 4,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4526),
                            Requests = "[]",
                            Username = "OthelloWorld"
                        },
                        new
                        {
                            Token = "john",
                            Bot = 3,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4432),
                            Requests = "[]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "jimmy",
                            Bot = 2,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4510),
                            Requests = "[]",
                            Username = "Jimmy"
                        },
                        new
                        {
                            Token = "ted",
                            Bot = 4,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4511),
                            Requests = "[]",
                            Username = "Ted"
                        },
                        new
                        {
                            Token = "michael",
                            Bot = 3,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4512),
                            Requests = "[]",
                            Username = "Michael"
                        },
                        new
                        {
                            Token = "william",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4513),
                            Requests = "[]",
                            Username = "William"
                        },
                        new
                        {
                            Token = "sarah",
                            Bot = 3,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4514),
                            Requests = "[]",
                            Username = "Sarah"
                        },
                        new
                        {
                            Token = "lisa",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4514),
                            Requests = "[]",
                            Username = "Lisa"
                        },
                        new
                        {
                            Token = "nancy",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4515),
                            Requests = "[]",
                            Username = "Nancy"
                        },
                        new
                        {
                            Token = "Anthony",
                            Bot = 2,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4516),
                            Requests = "[]",
                            Username = "Anthony"
                        },
                        new
                        {
                            Token = "matthew",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4517),
                            Requests = "[]",
                            Username = "Matthew"
                        },
                        new
                        {
                            Token = "donald",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4517),
                            Requests = "[]",
                            Username = "Donald"
                        },
                        new
                        {
                            Token = "andrew",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4518),
                            Requests = "[]",
                            Username = "Andrew"
                        },
                        new
                        {
                            Token = "kimberly",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4519),
                            Requests = "[]",
                            Username = "Kimberly"
                        },
                        new
                        {
                            Token = "margaret",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4520),
                            Requests = "[]",
                            Username = "Margaret"
                        },
                        new
                        {
                            Token = "carol",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4520),
                            Requests = "[]",
                            Username = "Carol"
                        },
                        new
                        {
                            Token = "brian",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4521),
                            Requests = "[]",
                            Username = "Brian"
                        },
                        new
                        {
                            Token = "jason",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4522),
                            Requests = "[]",
                            Username = "Jason"
                        },
                        new
                        {
                            Token = "jeffrey",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4522),
                            Requests = "[]",
                            Username = "Jeffrey"
                        },
                        new
                        {
                            Token = "amy",
                            Bot = 1,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4523),
                            Requests = "[]",
                            Username = "Amy"
                        },
                        new
                        {
                            Token = "mod-987456269420135",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4525),
                            Requests = "[]",
                            Username = "mod"
                        },
                        new
                        {
                            Token = "admin-133742069420135",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4524),
                            Requests = "[]",
                            Username = "admin"
                        },
                        new
                        {
                            Token = "deleted",
                            Bot = 0,
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 11, 26, 14, 21, 40, 120, DateTimeKind.Utc).AddTicks(4527),
                            Requests = "[]",
                            Username = "Deleted"
                        });
                });

            modelBuilder.Entity("API.Models.PlayerLog", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Token");

                    b.HasIndex("Player");

                    b.ToTable("Logs");
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

            modelBuilder.Entity("API.Models.PlayerLog", b =>
                {
                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("Player")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
        }
    }
}
