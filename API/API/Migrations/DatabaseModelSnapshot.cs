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

                    b.HasData(
                        new
                        {
                            Token = "zero",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4656),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4661),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4701),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4707),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4710),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4714),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4717),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4721),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4724),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4727),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4731),
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
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4763),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4763),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "2",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4761),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "1",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4762),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4762),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-3",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4765),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-4",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4765),
                            Draw = false,
                            Loser = "karen",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-5",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4766),
                            Draw = false,
                            Loser = "john",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-6",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4766),
                            Draw = false,
                            Loser = "karen",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-7",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4767),
                            Draw = false,
                            Loser = "burst",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-8",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4767),
                            Draw = false,
                            Loser = "cena",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-9",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4768),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-10",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4768),
                            Draw = false,
                            Loser = "john",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-11",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4769),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-12",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4769),
                            Draw = false,
                            Loser = "john",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-13",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4770),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-14",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4770),
                            Draw = false,
                            Loser = "karen",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4764),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-15",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4771),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-16",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4771),
                            Draw = false,
                            Loser = "john",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-17",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4772),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-18",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4772),
                            Draw = false,
                            Loser = "karen",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-19",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4773),
                            Draw = false,
                            Loser = "cena",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-32",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4781),
                            Draw = true,
                            Loser = "pedro",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-20",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4774),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-21",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4775),
                            Draw = false,
                            Loser = "john",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-22",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4776),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-23",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4776),
                            Draw = false,
                            Loser = "burst",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-24",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4777),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-31",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4781),
                            Draw = true,
                            Loser = "nastrovia",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-25",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4777),
                            Draw = false,
                            Loser = "burton",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-26",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4778),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-27",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4779),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-28",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4779),
                            Draw = true,
                            Loser = "gissa",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-29",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4780),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-30",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4780),
                            Draw = false,
                            Loser = "noga",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-33",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4782),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-34",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4782),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-35",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4783),
                            Draw = true,
                            Loser = "rachel",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-36",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4783),
                            Draw = true,
                            Loser = "salie",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-37",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4835),
                            Draw = false,
                            Loser = "hidde",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-38",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4836),
                            Draw = false,
                            Loser = "pedro",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-39",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4837),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-40",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4838),
                            Draw = false,
                            Loser = "rachel",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-41",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4839),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-42",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4840),
                            Draw = false,
                            Loser = "nastrovia",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-43",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4841),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-44",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4841),
                            Draw = false,
                            Loser = "noga",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-45",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4842),
                            Draw = false,
                            Loser = "salie",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-46",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4842),
                            Draw = true,
                            Loser = "pipo",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-47",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4843),
                            Draw = false,
                            Loser = "gissa",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-48",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4843),
                            Draw = true,
                            Loser = "hidde",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-49",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4844),
                            Draw = false,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-50",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4845),
                            Draw = false,
                            Loser = "nastrovia",
                            Winner = "cena"
                        },
                        new
                        {
                            Token = "-51",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4846),
                            Draw = true,
                            Loser = "pedro",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-52",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4846),
                            Draw = false,
                            Loser = "ahmed",
                            Winner = "burton"
                        },
                        new
                        {
                            Token = "-53",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4847),
                            Draw = false,
                            Loser = "nadege",
                            Winner = "briar"
                        },
                        new
                        {
                            Token = "-54",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4847),
                            Draw = true,
                            Loser = "rachel",
                            Winner = "lambert"
                        },
                        new
                        {
                            Token = "-55",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4848),
                            Draw = false,
                            Loser = "salie",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-56",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4848),
                            Draw = false,
                            Loser = "ernst",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-57",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4849),
                            Draw = true,
                            Loser = "john",
                            Winner = "gissa"
                        },
                        new
                        {
                            Token = "-58",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4849),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-59",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4850),
                            Draw = false,
                            Loser = "burst",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-60",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4850),
                            Draw = false,
                            Loser = "burton",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-61",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4851),
                            Draw = true,
                            Loser = "briar",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-62",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4852),
                            Draw = false,
                            Loser = "identity",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-63",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4852),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-64",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4853),
                            Draw = false,
                            Loser = "john",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-65",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4853),
                            Draw = true,
                            Loser = "cena",
                            Winner = "karen"
                        },
                        new
                        {
                            Token = "-66",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4854),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "burst"
                        },
                        new
                        {
                            Token = "-67",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4854),
                            Draw = false,
                            Loser = "burton",
                            Winner = "ernst"
                        },
                        new
                        {
                            Token = "-68",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4855),
                            Draw = true,
                            Loser = "lambert",
                            Winner = "john"
                        },
                        new
                        {
                            Token = "-69",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4855),
                            Draw = false,
                            Loser = "gissa",
                            Winner = "identity"
                        },
                        new
                        {
                            Token = "-70",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4856),
                            Draw = true,
                            Loser = "ernst",
                            Winner = "salie"
                        },
                        new
                        {
                            Token = "-71",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4856),
                            Draw = false,
                            Loser = "john",
                            Winner = "pipo"
                        },
                        new
                        {
                            Token = "-72",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4857),
                            Draw = false,
                            Loser = "eltjo",
                            Winner = "hidde"
                        },
                        new
                        {
                            Token = "-73",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4858),
                            Draw = true,
                            Loser = "burst",
                            Winner = "nastrovia"
                        },
                        new
                        {
                            Token = "-74",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4858),
                            Draw = false,
                            Loser = "briar",
                            Winner = "nadege"
                        },
                        new
                        {
                            Token = "-75",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4859),
                            Draw = false,
                            Loser = "tijn",
                            Winner = "noga"
                        },
                        new
                        {
                            Token = "-76",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4859),
                            Draw = false,
                            Loser = "lambert",
                            Winner = "pedro"
                        },
                        new
                        {
                            Token = "-77",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4860),
                            Draw = true,
                            Loser = "cena",
                            Winner = "ahmed"
                        },
                        new
                        {
                            Token = "-78",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4860),
                            Draw = false,
                            Loser = "burton",
                            Winner = "rachel"
                        },
                        new
                        {
                            Token = "-79",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4861),
                            Draw = true,
                            Loser = "noga",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-80",
                            Board = "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]",
                            Date = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4861),
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
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4402),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-31T06:21:11.7434408Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434411Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-31T06:21:11.7434411Z\"}]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[\"John\",\"Karen\",\"Burton\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4412),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.7434415Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-31T06:21:11.7434416Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434417Z\"}]",
                            Username = "Ernst"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[\"Ernst\",\"Karen\",\"Cena\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4418),
                            Requests = "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-31T06:21:11.743442Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434421Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434421Z\"}]",
                            Username = "John"
                        },
                        new
                        {
                            Token = "eltjo",
                            Friends = "[\"Tijn\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4460),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-31T06:21:11.7434463Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-31T06:21:11.7434464Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-31T06:21:11.7434465Z\"}]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[\"Eltjo\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4465),
                            Requests = "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-31T06:21:11.7434468Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-31T06:21:11.7434469Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434469Z\"}]",
                            Username = "Tijn"
                        },
                        new
                        {
                            Token = "cena",
                            Friends = "[\"John\",\"Karen\",\"Burst\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4470),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434472Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-31T06:21:11.7434472Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434473Z\"}]",
                            Username = "Cena"
                        },
                        new
                        {
                            Token = "burst",
                            Friends = "[\"Cena\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4474),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434475Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-31T06:21:11.7434476Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434477Z\"}]",
                            Username = "Burst"
                        },
                        new
                        {
                            Token = "burton",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4478),
                            Requests = "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-31T06:21:11.7434479Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-31T06:21:11.743448Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.743448Z\"}]",
                            Username = "Burton"
                        },
                        new
                        {
                            Token = "briar",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4481),
                            Requests = "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-31T06:21:11.7434483Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.7434483Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-31T06:21:11.7434484Z\"}]",
                            Username = "Briar"
                        },
                        new
                        {
                            Token = "lambert",
                            Friends = "[\"Eltjo\",\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4485),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.7434486Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434487Z\"}]",
                            Username = "Lambert"
                        },
                        new
                        {
                            Token = "identity",
                            Friends = "[\"Eltjo\",\"Tijn\",\"admin\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4488),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.743449Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-10-31T06:21:11.743449Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-31T06:21:11.7434491Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-31T06:21:11.7434491Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-31T06:21:11.7434492Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-31T06:21:11.7434493Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434494Z\"}]",
                            Username = "Identity"
                        },
                        new
                        {
                            Token = "salie",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4495),
                            Requests = "[]",
                            Username = "Salie"
                        },
                        new
                        {
                            Token = "pipo",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4495),
                            Requests = "[]",
                            Username = "Pipo"
                        },
                        new
                        {
                            Token = "gissa",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4497),
                            Requests = "[]",
                            Username = "Gissa"
                        },
                        new
                        {
                            Token = "hidde",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4498),
                            Requests = "[]",
                            Username = "Hidde"
                        },
                        new
                        {
                            Token = "noga",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4499),
                            Requests = "[]",
                            Username = "Noga"
                        },
                        new
                        {
                            Token = "nastrovia",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4499),
                            Requests = "[]",
                            Username = "Nastrovia"
                        },
                        new
                        {
                            Token = "pedro",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4500),
                            Requests = "[]",
                            Username = "Pedro"
                        },
                        new
                        {
                            Token = "ahmed",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4501),
                            Requests = "[]",
                            Username = "Ahmed"
                        },
                        new
                        {
                            Token = "nadege",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4502),
                            Requests = "[]",
                            Username = "Nadege"
                        },
                        new
                        {
                            Token = "rachel",
                            Friends = "[]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4503),
                            Requests = "[]",
                            Username = "Rachel"
                        },
                        new
                        {
                            Token = "ff20c418-f1b0-4f16-b582-294be25c24ef",
                            Friends = "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"admin\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4503),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.7434507Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434508Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-10-31T06:21:11.7434508Z\"}]",
                            Username = "mediator"
                        },
                        new
                        {
                            Token = "58a479fd-ae6f-4474-a147-68cbdb62c19b",
                            Friends = "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"mediator\"]",
                            LastActivity = new DateTime(2024, 10, 31, 6, 21, 11, 743, DateTimeKind.Utc).AddTicks(4509),
                            Requests = "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-31T06:21:11.7434512Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-10-31T06:21:11.7434513Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-10-31T06:21:11.7434513Z\"}]",
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
