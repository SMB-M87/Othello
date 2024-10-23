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

                    b.Property<int>("SColor")
                        .HasColumnType("int");

                    b.Property<string>("Second")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.HasIndex("First");

                    b.HasIndex("Second");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Token = "zero",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I wanna play a game and don't have any requirements!",
                            FColor = 2,
                            First = "karen",
                            PlayersTurn = 2,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "one",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I search an advanced player!",
                            FColor = 2,
                            First = "ernst",
                            PlayersTurn = 2,
                            SColor = 1,
                            Second = "john",
                            Status = 1
                        },
                        new
                        {
                            Token = "two",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I want to player more than one game against the same adversary.",
                            FColor = 2,
                            First = "eltjo",
                            PlayersTurn = 1,
                            SColor = 1,
                            Second = "tijn",
                            Status = 1
                        },
                        new
                        {
                            Token = "three",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I wanna play a game and don't have any requirements!",
                            FColor = 2,
                            First = "cena",
                            PlayersTurn = 2,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "four",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I wanna play a game and don't have any requirements!",
                            FColor = 2,
                            First = "burst",
                            PlayersTurn = 2,
                            SColor = 1,
                            Status = 0
                        });
                });

            modelBuilder.Entity("API.Models.GameResult", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Draw")
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

                    b.HasData(
                        new
                        {
                            Token = "-2",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8532),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8533),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8533),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8530),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8531),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8532),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8534),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8534),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8535),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8535),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8536),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8536),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8537),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8537),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8538),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8538),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8539),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8539),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8540),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8540),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8541),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8542),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8542),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-20",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8543),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8543),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8544),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8545),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8545),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-25",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8546),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Date = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8546),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        });
                });

            modelBuilder.Entity("API.Models.Player", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

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
                            Friends = "[\"Ernst\",\"John\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8261),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258264Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258267Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258268Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8269),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258271Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-23T21:17:51.2258271Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258272Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8273),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258275Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258276Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258276Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8277),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T21:17:51.2258278Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T21:17:51.2258279Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-23T21:17:51.225828Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8281),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T21:17:51.2258282Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258283Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258283Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8284),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258286Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258286Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258287Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8288),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.225829Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258291Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258291Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8292),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258293Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258294Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258295Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "sadbux",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8296),
                            Requests = "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-23T21:17:51.2258297Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258298Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T21:17:51.2258299Z\"}]",
                            Username = "Sadbux"
                        },
                        new
                        {
                            Token = "badbux",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8300),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258301Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258302Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258302Z\"}]",
                            Username = "Badbux"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8303),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258304Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258305Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258306Z\"}]",
                            Username = "Identity"
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
