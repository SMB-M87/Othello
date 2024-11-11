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

                    b.HasData(
                        new
                        {
                            Token = "-2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3566),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3567),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3563),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3564),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3565),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3568),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3569),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3569),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3570),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3606),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3607),
                            Draw = false,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3608),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3608),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3609),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3609),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3610),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3612),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3567),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3612),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3613),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3613),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3614),
                            Draw = false,
                            Forfeit = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3615),
                            Draw = false,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-32",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3622),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-20",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3615),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3616),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3616),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3617),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3617),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-31",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3622),
                            Draw = true,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-25",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3618),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3619),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-27",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3619),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-28",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3620),
                            Draw = true,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-29",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3620),
                            Draw = false,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-30",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3621),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-33",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3623),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-34",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3623),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nadege",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-35",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3624),
                            Draw = true,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-36",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3624),
                            Draw = true,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-37",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3625),
                            Draw = false,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-38",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3626),
                            Draw = false,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-39",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3626),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-40",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3627),
                            Draw = false,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-41",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3627),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-42",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3628),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-43",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3629),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-44",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3629),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-45",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3630),
                            Draw = false,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-46",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3630),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pipo",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-47",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3631),
                            Draw = false,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-48",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3631),
                            Draw = true,
                            Forfeit = false,
                            Loser = "hidde",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-49",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3632),
                            Draw = false,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-50",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3633),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nastrovia",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-51",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3633),
                            Draw = true,
                            Forfeit = false,
                            Loser = "pedro",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-52",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3634),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ahmed",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-53",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3634),
                            Draw = false,
                            Forfeit = false,
                            Loser = "nadege",
                            Winner = "briar"
                        },
                        new
                        {
                            Token = "-54",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3635),
                            Draw = true,
                            Forfeit = false,
                            Loser = "rachel",
                            Winner = "lambert"
                        },
                        new
                        {
                            Token = "-55",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3636),
                            Draw = false,
                            Forfeit = false,
                            Loser = "salie",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-56",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3636),
                            Draw = false,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-57",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3637),
                            Draw = true,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-58",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3637),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-59",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3638),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-60",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3638),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-61",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3639),
                            Draw = true,
                            Forfeit = false,
                            Loser = "briar",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-62",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3640),
                            Draw = false,
                            Forfeit = false,
                            Loser = "identity",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-63",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3640),
                            Draw = true,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-64",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3641),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-65",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3642),
                            Draw = true,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-66",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3642),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-67",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3643),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-68",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3643),
                            Draw = true,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-69",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3644),
                            Draw = false,
                            Forfeit = false,
                            Loser = "gissa",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-70",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3644),
                            Draw = true,
                            Forfeit = false,
                            Loser = "ernst",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-71",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3645),
                            Draw = false,
                            Forfeit = false,
                            Loser = "john",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-72",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3646),
                            Draw = false,
                            Forfeit = false,
                            Loser = "eltjo",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-73",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3646),
                            Draw = true,
                            Forfeit = false,
                            Loser = "burst",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-74",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3647),
                            Draw = false,
                            Forfeit = false,
                            Loser = "briar",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-75",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3648),
                            Draw = false,
                            Forfeit = false,
                            Loser = "tijn",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-76",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3648),
                            Draw = false,
                            Forfeit = false,
                            Loser = "lambert",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-77",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3649),
                            Draw = true,
                            Forfeit = false,
                            Loser = "cena",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-78",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3649),
                            Draw = false,
                            Forfeit = false,
                            Loser = "burton",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-79",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3650),
                            Draw = true,
                            Forfeit = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-80",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3650),
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

                    b.Property<string>("Friends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBot")
                        .HasColumnType("bit");

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
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3414),
                            Requests = "[]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3417),
                            Requests = "[]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3418),
                            Requests = "[]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3419),
                            Requests = "[]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3420),
                            Requests = "[]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3420),
                            Requests = "[]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3421),
                            Requests = "[]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3422),
                            Requests = "[]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "briar",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3423),
                            Requests = "[]",
                            Username = "Briar"
                        },
                        new
                        {
                            Token = "lambert",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3423),
                            Requests = "[]",
                            Username = "Lambert"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3424),
                            Requests = "[]",
                            Username = "Identity"
                        },
                        new
                        {
                            Token = "salie",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3425),
                            Requests = "[]",
                            Username = "Salie"
                        },
                        new
                        {
                            Token = "pipo",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3426),
                            Requests = "[]",
                            Username = "Pipo"
                        },
                        new
                        {
                            Token = "gissa",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3426),
                            Requests = "[]",
                            Username = "Gissa"
                        },
                        new
                        {
                            Token = "hidde",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3427),
                            Requests = "[]",
                            Username = "Hidde"
                        },
                        new
                        {
                            Token = "noga",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3428),
                            Requests = "[]",
                            Username = "Noga"
                        },
                        new
                        {
                            Token = "nastrovia",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3428),
                            Requests = "[]",
                            Username = "Nastrovia"
                        },
                        new
                        {
                            Token = "pedro",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3429),
                            Requests = "[]",
                            Username = "Pedro"
                        },
                        new
                        {
                            Token = "ahmed",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3430),
                            Requests = "[]",
                            Username = "Ahmed"
                        },
                        new
                        {
                            Token = "nadege",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3431),
                            Requests = "[]",
                            Username = "Nadege"
                        },
                        new
                        {
                            Token = "rachel",
                            Friends = "[]",
                            IsBot = true,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3431),
                            Requests = "[]",
                            Username = "Rachel"
                        },
                        new
                        {
                            Token = "ff20c418-f1b0-4f16-b582-294be25c24ef",
                            Friends = "[]",
                            IsBot = false,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3432),
                            Requests = "[]",
                            Username = "mediator"
                        },
                        new
                        {
                            Token = "58a479fd-ae6f-4474-a147-68cbdb62c19b",
                            Friends = "[]",
                            IsBot = false,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3433),
                            Requests = "[]",
                            Username = "admin"
                        },
                        new
                        {
                            Token = "deleted",
                            Friends = "[]",
                            IsBot = false,
                            LastActivity = new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3434),
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
