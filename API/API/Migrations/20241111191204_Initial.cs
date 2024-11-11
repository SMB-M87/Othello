using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Friends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlayersTurn = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FColor = table.Column<int>(type: "int", nullable: false),
                    Second = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SColor = table.Column<int>(type: "int", nullable: false),
                    Rematch = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Games_Players_First",
                        column: x => x.First,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_Rematch",
                        column: x => x.Rematch,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_Second",
                        column: x => x.Second,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Loser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Draw = table.Column<bool>(type: "bit", nullable: false),
                    Forfeit = table.Column<bool>(type: "bit", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Results_Players_Loser",
                        column: x => x.Loser,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Players_Winner",
                        column: x => x.Winner,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Token", "Friends", "IsBot", "LastActivity", "Requests", "Username" },
                values: new object[,]
                {
                    { "58a479fd-ae6f-4474-a147-68cbdb62c19b", "[]", false, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3700), "[]", "admin" },
                    { "ahmed", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3697), "[]", "Ahmed" },
                    { "briar", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3688), "[]", "Briar" },
                    { "burst", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3687), "[]", "Burst" },
                    { "burton", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3688), "[]", "Burton" },
                    { "cena", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3686), "[]", "Cena" },
                    { "deleted", "[]", false, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3701), "[]", "Deleted" },
                    { "eltjo", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3684), "[]", "Eltjo" },
                    { "ernst", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3682), "[]", "Ernst" },
                    { "ff20c418-f1b0-4f16-b582-294be25c24ef", "[]", false, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3699), "[]", "mediator" },
                    { "gissa", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3693), "[]", "Gissa" },
                    { "hidde", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3693), "[]", "Hidde" },
                    { "identity", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3690), "[]", "Identity" },
                    { "john", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3684), "[]", "John" },
                    { "karen", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3675), "[]", "Karen" },
                    { "lambert", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3689), "[]", "Lambert" },
                    { "nadege", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3698), "[]", "Nadege" },
                    { "nastrovia", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3695), "[]", "Nastrovia" },
                    { "noga", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3694), "[]", "Noga" },
                    { "pedro", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3696), "[]", "Pedro" },
                    { "pipo", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3692), "[]", "Pipo" },
                    { "rachel", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3698), "[]", "Rachel" },
                    { "salie", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3691), "[]", "Salie" },
                    { "tijn", "[]", true, new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3685), "[]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Date", "Description", "FColor", "First", "PlayersTurn", "Rematch", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "eight", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3987), "أريد أن ألعب لعبة وليس لدي أي متطلبات!", 1, "briar", 1, null, 2, null, 0 },
                    { "four", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3914), "میں ایک کھیل کھیلنا چاہتا ہوں اور کوئی ضرورت نہیں ہے!", 2, "burst", 2, null, 1, null, 0 },
                    { "nine", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3993), "I search an advanced player to play more than one game against!", 2, "lambert", 2, null, 1, null, 0 },
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3897), "I search an advanced player!", 1, "ernst", 2, null, 2, null, 0 },
                    { "seven", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3926), "Je veux jouer une partie contre un élite!!!", 2, "nastrovia", 1, null, 1, null, 0 },
                    { "six", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3920), "Θέλω να παίξω ένα παιχνίδι και δεν έχω απαιτήσεις!!!", 1, "burton", 1, null, 2, null, 0 },
                    { "t11", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4004), "I want to player more than one game against the same adversary.", 2, "pipo", 1, null, 1, null, 0 },
                    { "ten", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3999), "אני רוצה לשחק משחק ואין לי שום דרישות!", 1, "salie", 1, null, 2, null, 0 },
                    { "three", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3909), "I search an advanced player!", 1, "cena", 1, null, 2, null, 0 },
                    { "two", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3904), "I want to player more than one game against the same adversary.", 2, "eltjo", 1, null, 1, null, 0 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(3890), "I wanna play a game and don't have any requirements!", 2, "karen", 2, null, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Board", "Date", "Draw", "Forfeit", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4059), false, false, "eltjo", "tijn" },
                    { "-1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4058), false, false, "eltjo", "tijn" },
                    { "-10", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4067), false, false, "john", "eltjo" },
                    { "-11", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4067), false, false, "ernst", "john" },
                    { "-12", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4069), false, false, "john", "cena" },
                    { "-13", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4070), false, false, "tijn", "burst" },
                    { "-14", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4070), false, false, "karen", "tijn" },
                    { "-15", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4071), false, false, "ernst", "burton" },
                    { "-16", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4071), false, false, "john", "burton" },
                    { "-17", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4075), false, false, "ernst", "cena" },
                    { "-18", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4076), false, false, "karen", "eltjo" },
                    { "-19", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4077), false, false, "cena", "burst" },
                    { "-2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4057), false, false, "tijn", "eltjo" },
                    { "-20", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4077), false, false, "burton", "ernst" },
                    { "-21", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4078), false, false, "john", "tijn" },
                    { "-22", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4079), false, false, "eltjo", "karen" },
                    { "-23", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4080), false, false, "burst", "john" },
                    { "-24", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4082), false, false, "tijn", "cena" },
                    { "-25", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4083), false, false, "burton", "karen" },
                    { "-26", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4083), false, false, "eltjo", "burst" },
                    { "-27", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4084), true, false, "pipo", "salie" },
                    { "-28", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4087), true, false, "gissa", "pipo" },
                    { "-29", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4087), false, false, "hidde", "gissa" },
                    { "-3", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4060), false, false, "ernst", "karen" },
                    { "-30", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4088), false, false, "noga", "hidde" },
                    { "-31", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4089), true, false, "nastrovia", "noga" },
                    { "-32", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4090), true, false, "pedro", "nastrovia" },
                    { "-33", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4090), false, false, "ahmed", "pedro" },
                    { "-34", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4093), false, false, "nadege", "ahmed" },
                    { "-35", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4094), true, false, "rachel", "nadege" },
                    { "-36", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4095), true, false, "salie", "rachel" },
                    { "-37", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4096), false, false, "hidde", "pipo" },
                    { "-38", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4096), false, false, "pedro", "noga" },
                    { "-39", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4098), false, false, "ahmed", "nastrovia" },
                    { "-4", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4061), false, false, "karen", "john" },
                    { "-40", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4099), false, false, "rachel", "pedro" },
                    { "-41", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4100), true, false, "pipo", "ahmed" },
                    { "-42", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4100), false, false, "nastrovia", "gissa" },
                    { "-43", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4101), false, false, "ahmed", "hidde" },
                    { "-44", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4102), false, false, "noga", "rachel" },
                    { "-45", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4102), false, false, "salie", "karen" },
                    { "-46", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4103), true, false, "pipo", "ernst" },
                    { "-47", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4104), false, false, "gissa", "john" },
                    { "-48", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4104), true, false, "hidde", "eltjo" },
                    { "-49", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4105), false, false, "noga", "tijn" },
                    { "-5", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4061), false, false, "john", "ernst" },
                    { "-50", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4106), false, false, "nastrovia", "cena" },
                    { "-51", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4106), true, false, "pedro", "burst" },
                    { "-52", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4107), false, false, "ahmed", "burton" },
                    { "-53", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4108), false, false, "nadege", "briar" },
                    { "-54", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4108), true, false, "rachel", "lambert" },
                    { "-55", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4109), false, false, "salie", "identity" },
                    { "-56", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4110), false, false, "ernst", "pipo" },
                    { "-57", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4110), true, false, "john", "gissa" },
                    { "-58", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4111), false, false, "tijn", "hidde" },
                    { "-59", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4112), false, false, "burst", "noga" },
                    { "-6", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4062), false, false, "karen", "cena" },
                    { "-60", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4112), false, false, "burton", "nastrovia" },
                    { "-61", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4113), true, false, "briar", "pedro" },
                    { "-62", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4114), false, false, "identity", "ahmed" },
                    { "-63", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4114), true, false, "lambert", "nadege" },
                    { "-64", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4115), false, false, "john", "rachel" },
                    { "-65", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4116), true, false, "cena", "karen" },
                    { "-66", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4116), false, false, "tijn", "burst" },
                    { "-67", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4117), false, false, "burton", "ernst" },
                    { "-68", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4118), true, false, "lambert", "john" },
                    { "-69", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4119), false, false, "gissa", "identity" },
                    { "-7", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4064), false, false, "burst", "karen" },
                    { "-70", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4119), true, false, "ernst", "salie" },
                    { "-71", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4120), false, false, "john", "pipo" },
                    { "-72", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4121), false, false, "eltjo", "hidde" },
                    { "-73", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4122), true, false, "burst", "nastrovia" },
                    { "-74", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4122), false, false, "briar", "nadege" },
                    { "-75", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4173), false, false, "tijn", "noga" },
                    { "-76", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4175), false, false, "lambert", "pedro" },
                    { "-77", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4176), true, false, "cena", "ahmed" },
                    { "-78", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4176), false, false, "burton", "rachel" },
                    { "-79", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4177), true, false, "noga", "tijn" },
                    { "-8", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4065), false, false, "cena", "ernst" },
                    { "-80", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4178), false, false, "pedro", "burton" },
                    { "-9", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4066), false, false, "eltjo", "tijn" },
                    { "0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4057), false, false, "eltjo", "tijn" },
                    { "1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4056), false, false, "eltjo", "tijn" },
                    { "2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 19, 12, 4, 31, DateTimeKind.Utc).AddTicks(4054), false, false, "tijn", "eltjo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_First",
                table: "Games",
                column: "First");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Rematch",
                table: "Games",
                column: "Rematch");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Second",
                table: "Games",
                column: "Second");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Username",
                table: "Players",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_Loser",
                table: "Results",
                column: "Loser");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Winner",
                table: "Results",
                column: "Winner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
