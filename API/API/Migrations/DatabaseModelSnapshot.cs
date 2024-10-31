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
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2246),
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
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2250),
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
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2254),
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
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2259),
                            Description = "I search an advanced player!",
                            FColor = 2,
                            First = "cena",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "four",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2262),
                            Description = "میں ایک کھیل کھیلنا چاہتا ہوں اور کوئی ضرورت نہیں ہے!",
                            FColor = 2,
                            First = "burst",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "six",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2266),
                            Description = "Θέλω να παίξω ένα παιχνίδι και δεν έχω απαιτήσεις!!!",
                            FColor = 2,
                            First = "burton",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "seven",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2269),
                            Description = "Je veux jouer une partie contre un élite!!!",
                            FColor = 2,
                            First = "briar",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "eight",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2272),
                            Description = "أريد أن ألعب لعبة وليس لدي أي متطلبات!",
                            FColor = 2,
                            First = "briar",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "nine",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2275),
                            Description = "I search an advanced player to play more than one game against!",
                            FColor = 2,
                            First = "lambert",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "ten",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2279),
                            Description = "אני רוצה לשחק משחק ואין לי שום דרישות!",
                            FColor = 2,
                            First = "salie",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "t11",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2282),
                            Description = "I want to player more than one game against the same adversary.",
                            FColor = 2,
                            First = "pipo",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        });
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
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2307),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2308),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2305),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2306),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2306),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2309),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2309),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2310),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2310),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2311),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2311),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2312),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2343),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2344),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2345),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2345),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2346),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2308),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2346),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2347),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2347),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2348),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2348),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-32",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2356),
                            Draw = true,
                            Loser = "pedro",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-20",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2349),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2349),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2350),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2350),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2351),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-31",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2356),
                            Draw = true,
                            Loser = "nastrovia",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-25",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2352),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2353),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-27",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2353),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-28",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2354),
                            Draw = true,
                            Loser = "gissa",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-29",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2354),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-30",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2355),
                            Draw = false,
                            Loser = "noga",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-33",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2357),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-34",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2357),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-35",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2358),
                            Draw = true,
                            Loser = "rachel",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-36",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2358),
                            Draw = true,
                            Loser = "salie",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-37",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2359),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-38",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2359),
                            Draw = false,
                            Loser = "pedro",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-39",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2360),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-40",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2360),
                            Draw = false,
                            Loser = "rachel",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-41",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2361),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-42",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2361),
                            Draw = false,
                            Loser = "nastrovia",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-43",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2362),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-44",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2362),
                            Draw = false,
                            Loser = "noga",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-45",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2363),
                            Draw = false,
                            Loser = "salie",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-46",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2363),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-47",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2364),
                            Draw = false,
                            Loser = "gissa",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-48",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2365),
                            Draw = true,
                            Loser = "hidde",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-49",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2365),
                            Draw = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-50",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2366),
                            Draw = false,
                            Loser = "nastrovia",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-51",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2366),
                            Draw = true,
                            Loser = "pedro",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-52",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2367),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-53",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2367),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "briar"
                        },
                        new
                        {
                            Token = "-54",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2368),
                            Draw = true,
                            Loser = "rachel",
                            Winner = "lambert"
                        },
                        new
                        {
                            Token = "-55",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2368),
                            Draw = false,
                            Loser = "salie",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-56",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2369),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-57",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2369),
                            Draw = true,
                            Loser = "john",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-58",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2370),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-59",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2370),
                            Draw = false,
                            Loser = "burst",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-60",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2370),
                            Draw = false,
                            Loser = "burton",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-61",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2371),
                            Draw = true,
                            Loser = "briar",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-62",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2371),
                            Draw = false,
                            Loser = "identity",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-63",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2372),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-64",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2373),
                            Draw = false,
                            Loser = "john",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-65",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2374),
                            Draw = true,
                            Loser = "cena",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-66",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2374),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-67",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2374),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-68",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2375),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-69",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2375),
                            Draw = false,
                            Loser = "gissa",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-70",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2376),
                            Draw = true,
                            Loser = "ernst",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-71",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2376),
                            Draw = false,
                            Loser = "john",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-72",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2377),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-73",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2377),
                            Draw = true,
                            Loser = "burst",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-74",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2378),
                            Draw = false,
                            Loser = "briar",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-75",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2378),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-76",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2379),
                            Draw = false,
                            Loser = "lambert",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-77",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2379),
                            Draw = true,
                            Loser = "cena",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-78",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2380),
                            Draw = false,
                            Loser = "burton",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-79",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2380),
                            Draw = true,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-80",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2381),
                            Draw = false,
                            Loser = "pedro",
                            Winner = "burton"
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
                            Friends = "[\"Ernst\",\"John\",\"Cena\",\"Burst\",\"Burton\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2015),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-30T21:43:31.3082021Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082024Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-30T21:43:31.3082024Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\",\"Burton\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2025),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.3082028Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-30T21:43:31.3082029Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082029Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\",\"Cena\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2030),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-30T21:43:31.3082032Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082033Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082034Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2035),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-30T21:43:31.3082037Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-30T21:43:31.3082038Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-30T21:43:31.3082039Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2039),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-30T21:43:31.3082042Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-30T21:43:31.3082043Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082043Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\",\"Burst\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2044),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082045Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-30T21:43:31.3082046Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082047Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2048),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082049Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-30T21:43:31.308205Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.308205Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2051),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-30T21:43:31.3082052Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-30T21:43:31.3082053Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082054Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "briar",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2054),
                            Requests = "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-30T21:43:31.3082056Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.3082057Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-30T21:43:31.3082057Z\"}]",
                            Username = "Briar"
                        },
                        new
                        {
                            Token = "lambert",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2058),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.3082059Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.308206Z\"}]",
                            Username = "Lambert"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[\"Eltjo\",\"Tijn\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2061),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.3082062Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-30T21:43:31.3082063Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-30T21:43:31.3082064Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-30T21:43:31.3082064Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-30T21:43:31.3082064Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-30T21:43:31.3082065Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082066Z\"}]",
                            Username = "Identity"
                        },
                        new
                        {
                            Token = "salie",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2067),
                            Requests = "[]",
                            Username = "Salie"
                        },
                        new
                        {
                            Token = "pipo",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2067),
                            Requests = "[]",
                            Username = "Pipo"
                        },
                        new
                        {
                            Token = "gissa",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2068),
                            Requests = "[]",
                            Username = "Gissa"
                        },
                        new
                        {
                            Token = "hidde",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2069),
                            Requests = "[]",
                            Username = "Hidde"
                        },
                        new
                        {
                            Token = "noga",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2069),
                            Requests = "[]",
                            Username = "Noga"
                        },
                        new
                        {
                            Token = "nastrovia",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2070),
                            Requests = "[]",
                            Username = "Nastrovia"
                        },
                        new
                        {
                            Token = "pedro",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2070),
                            Requests = "[]",
                            Username = "Pedro"
                        },
                        new
                        {
                            Token = "ahmed",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2071),
                            Requests = "[]",
                            Username = "Ahmed"
                        },
                        new
                        {
                            Token = "nadege",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2072),
                            Requests = "[]",
                            Username = "Nadege"
                        },
                        new
                        {
                            Token = "rachel",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2072),
                            Requests = "[]",
                            Username = "Rachel"
                        },
                        new
                        {
                            Token = "12ae08b8-85e2-47e2-aadf-3464a93ce526",
                            Friends = "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"admin\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2073),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.3082076Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082076Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-10-30T21:43:31.3082077Z\"}]",
                            Username = "mediator"
                        },
                        new
                        {
                            Token = "2951f50b-a81e-4501-b2fd-510536a5936b",
                            Friends = "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 30, 21, 43, 31, 308, DateTimeKind.Utc).AddTicks(2078),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-30T21:43:31.308208Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-30T21:43:31.3082081Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-10-30T21:43:31.3082081Z\"}]",
                            Username = "admin"
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
