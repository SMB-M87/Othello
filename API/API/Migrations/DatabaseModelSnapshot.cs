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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8281),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8287),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8331),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8338),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8342),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8345),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8348),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8352),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8355),
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
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8359),
                            Description = "אני רוצה לשחק משחק ואין לי שום דרישות!",
                            FColor = 2,
                            First = "bert",
                            PlayersTurn = 1,
                            SColor = 1,
                            Status = 0
                        },
                        new
                        {
                            Token = "t11",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8362),
                            Description = "I want to player more than one game against the same adversary.",
                            FColor = 2,
                            First = "manolo",
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
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8385),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8386),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8383),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8384),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8384),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8387),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8388),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8389),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8390),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8390),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8391),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8392),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8393),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8393),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8394),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8395),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8395),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8386),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8396),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8396),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8397),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8398),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8399),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-32",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8408),
                            Draw = true,
                            Loser = "dasha",
                            Winner = "lea"
                        },
                        new
                        {
                            Token = "-20",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8399),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8400),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8400),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8401),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8401),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-31",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8407),
                            Draw = true,
                            Loser = "lea",
                            Winner = "nora"
                        },
                        new
                        {
                            Token = "-25",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8402),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8403),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-27",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8405),
                            Draw = true,
                            Loser = "manolo",
                            Winner = "bert"
                        },
                        new
                        {
                            Token = "-28",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8406),
                            Draw = true,
                            Loser = "gijs",
                            Winner = "manolo"
                        },
                        new
                        {
                            Token = "-29",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8406),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "gijs"
                        },
                        new
                        {
                            Token = "-30",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8407),
                            Draw = false,
                            Loser = "nora",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-33",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8409),
                            Draw = false,
                            Loser = "adrianna",
                            Winner = "dasha"
                        },
                        new
                        {
                            Token = "-34",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8410),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "adrianna"
                        },
                        new
                        {
                            Token = "-35",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8410),
                            Draw = true,
                            Loser = "macron",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-36",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8411),
                            Draw = true,
                            Loser = "bert",
                            Winner = "macron"
                        },
                        new
                        {
                            Token = "-37",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8412),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "manolo"
                        },
                        new
                        {
                            Token = "-38",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8412),
                            Draw = false,
                            Loser = "dasha",
                            Winner = "nora"
                        },
                        new
                        {
                            Token = "-39",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8413),
                            Draw = false,
                            Loser = "adrianna",
                            Winner = "lea"
                        },
                        new
                        {
                            Token = "-40",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8413),
                            Draw = false,
                            Loser = "macron",
                            Winner = "dasha"
                        },
                        new
                        {
                            Token = "-41",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8414),
                            Draw = true,
                            Loser = "manolo",
                            Winner = "adrianna"
                        },
                        new
                        {
                            Token = "-42",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8414),
                            Draw = false,
                            Loser = "lea",
                            Winner = "gijs"
                        },
                        new
                        {
                            Token = "-43",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8474),
                            Draw = false,
                            Loser = "adrianna",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-44",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8475),
                            Draw = false,
                            Loser = "nora",
                            Winner = "macron"
                        },
                        new
                        {
                            Token = "-45",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8476),
                            Draw = false,
                            Loser = "bert",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-46",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8476),
                            Draw = true,
                            Loser = "manolo",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-47",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8477),
                            Draw = false,
                            Loser = "gijs",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-48",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8478),
                            Draw = true,
                            Loser = "hidde",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-49",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8478),
                            Draw = false,
                            Loser = "nora",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-50",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8479),
                            Draw = false,
                            Loser = "lea",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-51",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8479),
                            Draw = true,
                            Loser = "dasha",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-52",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8480),
                            Draw = false,
                            Loser = "adrianna",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-53",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8481),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "briar"
                        },
                        new
                        {
                            Token = "-54",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8482),
                            Draw = true,
                            Loser = "macron",
                            Winner = "lambert"
                        },
                        new
                        {
                            Token = "-55",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8483),
                            Draw = false,
                            Loser = "bert",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-56",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8484),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "manolo"
                        },
                        new
                        {
                            Token = "-57",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8484),
                            Draw = true,
                            Loser = "john",
                            Winner = "gijs"
                        },
                        new
                        {
                            Token = "-58",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8485),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-59",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8485),
                            Draw = false,
                            Loser = "burst",
                            Winner = "nora"
                        },
                        new
                        {
                            Token = "-60",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8486),
                            Draw = false,
                            Loser = "burton",
                            Winner = "lea"
                        },
                        new
                        {
                            Token = "-61",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8487),
                            Draw = true,
                            Loser = "briar",
                            Winner = "dasha"
                        },
                        new
                        {
                            Token = "-62",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8487),
                            Draw = false,
                            Loser = "identity",
                            Winner = "adrianna"
                        },
                        new
                        {
                            Token = "-63",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8488),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-64",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8488),
                            Draw = false,
                            Loser = "john",
                            Winner = "macron"
                        },
                        new
                        {
                            Token = "-65",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8489),
                            Draw = true,
                            Loser = "cena",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-66",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8490),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-67",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8490),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-68",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8491),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-69",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8491),
                            Draw = false,
                            Loser = "gijs",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-70",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8492),
                            Draw = true,
                            Loser = "ernst",
                            Winner = "bert"
                        },
                        new
                        {
                            Token = "-71",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8493),
                            Draw = false,
                            Loser = "john",
                            Winner = "manolo"
                        },
                        new
                        {
                            Token = "-72",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8494),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-73",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8494),
                            Draw = true,
                            Loser = "burst",
                            Winner = "lea"
                        },
                        new
                        {
                            Token = "-74",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8495),
                            Draw = false,
                            Loser = "briar",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-75",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8495),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "nora"
                        },
                        new
                        {
                            Token = "-76",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8496),
                            Draw = false,
                            Loser = "lambert",
                            Winner = "dasha"
                        },
                        new
                        {
                            Token = "-77",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8497),
                            Draw = true,
                            Loser = "cena",
                            Winner = "adrianna"
                        },
                        new
                        {
                            Token = "-78",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8497),
                            Draw = false,
                            Loser = "burton",
                            Winner = "macron"
                        },
                        new
                        {
                            Token = "-79",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8498),
                            Draw = true,
                            Loser = "nora",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-80",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8498),
                            Draw = false,
                            Loser = "dasha",
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
                            Friends = "[\"Ernst\",\"John\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8082),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-27T03:29:28.5648086Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648092Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-27T03:29:28.5648093Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8094),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-27T03:29:28.5648095Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-27T03:29:28.5648096Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648096Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8097),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-27T03:29:28.5648099Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648099Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.56481Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8101),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-27T03:29:28.5648103Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-27T03:29:28.5648103Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-27T03:29:28.5648104Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8105),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-27T03:29:28.5648106Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-27T03:29:28.5648107Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.5648108Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8109),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648111Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-27T03:29:28.5648112Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.5648113Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8113),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648115Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-27T03:29:28.5648116Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.5648116Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8117),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-27T03:29:28.5648119Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-27T03:29:28.564812Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.564812Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "briar",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8121),
                            Requests = "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-27T03:29:28.5648122Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-27T03:29:28.5648123Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-27T03:29:28.5648124Z\"}]",
                            Username = "Briar"
                        },
                        new
                        {
                            Token = "lambert",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8124),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-27T03:29:28.5648126Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.5648127Z\"}]",
                            Username = "Lambert"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8128),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-27T03:29:28.5648129Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-27T03:29:28.564813Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-27T03:29:28.5648131Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-27T03:29:28.5648131Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-27T03:29:28.5648132Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-27T03:29:28.5648133Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-27T03:29:28.5648134Z\"}]",
                            Username = "Identity"
                        },
                        new
                        {
                            Token = "bert",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8135),
                            Requests = "[]",
                            Username = "Bert"
                        },
                        new
                        {
                            Token = "manolo",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8135),
                            Requests = "[]",
                            Username = "Manolo"
                        },
                        new
                        {
                            Token = "gijs",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8136),
                            Requests = "[]",
                            Username = "Gijs"
                        },
                        new
                        {
                            Token = "hidde",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8137),
                            Requests = "[]",
                            Username = "Hidde"
                        },
                        new
                        {
                            Token = "nora",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8137),
                            Requests = "[]",
                            Username = "Nora"
                        },
                        new
                        {
                            Token = "lea",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8139),
                            Requests = "[]",
                            Username = "Léa"
                        },
                        new
                        {
                            Token = "dasha",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8140),
                            Requests = "[]",
                            Username = "Dasha"
                        },
                        new
                        {
                            Token = "adrianna",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8140),
                            Requests = "[]",
                            Username = "Adrianna"
                        },
                        new
                        {
                            Token = "nadege",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8141),
                            Requests = "[]",
                            Username = "Nadege"
                        },
                        new
                        {
                            Token = "macron",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 27, 3, 29, 28, 564, DateTimeKind.Utc).AddTicks(8142),
                            Requests = "[]",
                            Username = "Macron"
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
