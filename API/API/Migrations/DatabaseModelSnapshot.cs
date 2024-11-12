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

                    b.HasData(
                        new
                        {
                            Token = "-2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5961),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5962),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5958),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5960),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5961),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5964),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5965),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5966),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5966),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5967),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5968),
                            Draw = false,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5968),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5969),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5969),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5970),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5971),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5971),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5963),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5972),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5972),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5973),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5973),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5974),
                            Draw = false,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-32",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5982),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-20",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5975),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5975),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5976),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5977),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5977),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-31",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5982),
                            Draw = true,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-25",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5978),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5978),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-27",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5979),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-28",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5980),
                            Draw = true,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-29",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5980),
                            Draw = false,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-30",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5981),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-33",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5983),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-34",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5984),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nadege",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-35",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5985),
                            Draw = true,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-36",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5989),
                            Draw = true,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-37",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5990),
                            Draw = false,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-38",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5991),
                            Draw = false,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-39",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5992),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-40",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5993),
                            Draw = false,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-41",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5994),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-42",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5995),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-43",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5997),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-44",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5997),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-45",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5998),
                            Draw = false,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-46",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5999),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-47",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6000),
                            Draw = false,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-48",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6000),
                            Draw = true,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-49",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6001),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-50",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6001),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-51",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6002),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-52",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6003),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-53",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6003),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nadege",
                            Winner = "briar"
                        },
                        new
                        {
                            Token = "-54",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6004),
                            Draw = true,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "lambert"
                        },
                        new
                        {
                            Token = "-55",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6005),
                            Draw = false,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-56",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6006),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-57",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6006),
                            Draw = true,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-58",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6008),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-59",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6008),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-60",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6009),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-61",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6009),
                            Draw = true,
                            Forfeit = false,
                            Loser = "briar",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-62",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6010),
                            Draw = false,
                            Forfeit = false,
                            Loser = "identity",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-63",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6010),
                            Draw = true,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-64",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6011),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-65",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6012),
                            Draw = true,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-66",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6012),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-67",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6013),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-68",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6013),
                            Draw = true,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-69",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6014),
                            Draw = false,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-70",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6015),
                            Draw = true,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-71",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6015),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-72",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6016),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-73",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6016),
                            Draw = true,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-74",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6017),
                            Draw = false,
                            Forfeit = false,
                            Loser = "briar",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-75",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6017),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-76",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6018),
                            Draw = false,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-77",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6019),
                            Draw = true,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-78",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6021),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-79",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6022),
                            Draw = true,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-80",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6022),
                            Draw = false,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "burton"
                        });
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
