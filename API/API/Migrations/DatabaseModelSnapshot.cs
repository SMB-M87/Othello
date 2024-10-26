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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7237),
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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7242),
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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7246),
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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7251),
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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7255),
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
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7273),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7274),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7274),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7271),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7272),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7273),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7275),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7316),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7317),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7317),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7318),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7319),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7319),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7320),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7320),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7321),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7321),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7322),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7322),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7324),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-20",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7324),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7325),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7325),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7326),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7326),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-25",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7327),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Date = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7327),
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
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7002),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527006Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527008Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527009Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7010),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527056Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-26T00:33:03.4527058Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527058Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7060),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527061Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527062Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527062Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7063),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527065Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527066Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-26T00:33:03.4527066Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7067),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527068Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527069Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.452707Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7071),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527072Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527073Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527073Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7074),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527076Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527077Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527078Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7079),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.452708Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527081Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527082Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "sadbux",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7083),
                            Requests = "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-26T00:33:03.4527084Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527085Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527085Z\"}]",
                            Username = "Sadbux"
                        },
                        new
                        {
                            Token = "badbux",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7086),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527087Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527088Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527089Z\"}]",
                            Username = "Badbux"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7090),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527091Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527092Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-26T00:33:03.4527092Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-26T00:33:03.4527093Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527093Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527095Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527095Z\"}]",
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
