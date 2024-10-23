﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
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
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1704),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1705),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1705),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1702),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1703),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1704),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1706),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1706),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1707),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1707),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1709),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1709),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1710),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1712),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1713),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1713),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1714),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1714),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1715),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1715),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1716),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1716),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1717),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-20",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1717),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1718),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1718),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1719),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1719),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-25",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1720),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Date = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1720),
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
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1428),
                            Requests = "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881432Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881436Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881436Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1438),
                            Requests = "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-21T08:51:45.588144Z\"},{\"Type\":0,\"Username\":\"burst\",\"Date\":\"2024-10-21T08:51:45.5881441Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881442Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1443),
                            Requests = "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881444Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881445Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-21T08:51:45.5881446Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1447),
                            Requests = "[{\"Type\":0,\"Username\":\"karen\",\"Date\":\"2024-10-21T08:51:45.5881448Z\"},{\"Type\":0,\"Username\":\"ernst\",\"Date\":\"2024-10-21T08:51:45.5881449Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-21T08:51:45.5881449Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1450),
                            Requests = "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881451Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881452Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881453Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1454),
                            Requests = "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881455Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881456Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881457Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1458),
                            Requests = "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881459Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.588146Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.588146Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1461),
                            Requests = "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881463Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881464Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881464Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "nice",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1465),
                            Requests = "[{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881467Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881467Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881468Z\"}]",
                            Username = "Sadbux"
                        },
                        new
                        {
                            Token = "bad",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1469),
                            Requests = "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-21T08:51:45.588147Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881471Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881471Z\"}]",
                            Username = "Badbux"
                        });
                });

            modelBuilder.Entity("API.Models.Game", b =>
                {
                    b.HasOne("API.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("First")
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
#pragma warning restore 612, 618
        }
    }
}
