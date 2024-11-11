using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Requests = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                columns: new[] { "Token", "Friends", "LastActivity", "Requests", "Username" },
                values: new object[,]
                {
                    { "58a479fd-ae6f-4474-a147-68cbdb62c19b", "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7608), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.8057611Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057612Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-11-09T19:19:43.8057612Z\"}]", "admin" },
                    { "ahmed", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7601), "[]", "Ahmed" },
                    { "briar", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7581), "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-11-09T19:19:43.8057583Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.8057584Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-11-09T19:19:43.8057584Z\"}]", "Briar" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7573), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057575Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-11-09T19:19:43.8057576Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057576Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7577), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057579Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-11-09T19:19:43.805758Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.805758Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\",\"Burst\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7569), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057571Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-11-09T19:19:43.8057572Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057572Z\"}]", "Cena" },
                    { "deleted", "[]", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "[]", "Deleted" },
                    { "eltjo", "[\"Tijn\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7559), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-11-09T19:19:43.8057562Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-11-09T19:19:43.8057563Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-11-09T19:19:43.8057563Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\",\"Burton\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7488), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.8057491Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-11-09T19:19:43.8057551Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057552Z\"}]", "Ernst" },
                    { "ff20c418-f1b0-4f16-b582-294be25c24ef", "[\"Karen\",\"Ernst\",\"John\",\"Identity\",\"Eltjo\",\"Tijn\",\"admin\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7603), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.8057606Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057607Z\"},{\"Type\":0,\"Username\":\"Rachel\",\"Date\":\"2024-11-09T19:19:43.8057607Z\"}]", "mediator" },
                    { "gissa", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7597), "[]", "Gissa" },
                    { "hidde", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7598), "[]", "Hidde" },
                    { "identity", "[\"Eltjo\",\"Tijn\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7589), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.805759Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-11-09T19:19:43.8057591Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-11-09T19:19:43.8057592Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-11-09T19:19:43.8057592Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-11-09T19:19:43.8057593Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-11-09T19:19:43.8057594Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057594Z\"}]", "Identity" },
                    { "john", "[\"Ernst\",\"Karen\",\"Cena\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7553), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-11-09T19:19:43.8057556Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057557Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057558Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\",\"Cena\",\"Burst\",\"Burton\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7479), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-11-09T19:19:43.8057484Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-11-09T19:19:43.8057486Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-11-09T19:19:43.8057487Z\"}]", "Karen" },
                    { "lambert", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7585), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-11-09T19:19:43.8057587Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057588Z\"}]", "Lambert" },
                    { "nadege", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7601), "[]", "Nadege" },
                    { "nastrovia", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7599), "[]", "Nastrovia" },
                    { "noga", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7598), "[]", "Noga" },
                    { "pedro", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7600), "[]", "Pedro" },
                    { "pipo", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7596), "[]", "Pipo" },
                    { "rachel", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7602), "[]", "Rachel" },
                    { "salie", "[]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7595), "[]", "Salie" },
                    { "tijn", "[\"Eltjo\",\"Identity\",\"Briar\",\"Lambert\",\"admin\",\"mediator\"]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7564), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-11-09T19:19:43.8057567Z\"},{\"Type\":0,\"Username\":\"Lambert\",\"Date\":\"2024-11-09T19:19:43.8057568Z\"},{\"Type\":0,\"Username\":\"Briar\",\"Date\":\"2024-11-09T19:19:43.8057568Z\"}]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Date", "Description", "FColor", "First", "PlayersTurn", "Rematch", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "eight", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7823), "أريد أن ألعب لعبة وليس لدي أي متطلبات!", 2, "briar", 1, null, 1, null, 0 },
                    { "four", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7812), "میں ایک کھیل کھیلنا چاہتا ہوں اور کوئی ضرورت نہیں ہے!", 2, "burst", 1, null, 1, null, 0 },
                    { "nine", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7826), "I search an advanced player to play more than one game against!", 2, "lambert", 1, null, 1, null, 0 },
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7798), "I search an advanced player!", 2, "ernst", 2, null, 1, "john", 1 },
                    { "seven", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7819), "Je veux jouer une partie contre un élite!!!", 2, "nastrovia", 1, null, 1, null, 0 },
                    { "six", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7816), "Θέλω να παίξω ένα παιχνίδι και δεν έχω απαιτήσεις!!!", 2, "burton", 1, null, 1, null, 0 },
                    { "t11", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7834), "I want to player more than one game against the same adversary.", 2, "pipo", 1, null, 1, null, 0 },
                    { "ten", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7830), "אני רוצה לשחק משחק ואין לי שום דרישות!", 2, "salie", 1, null, 1, null, 0 },
                    { "three", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7808), "I search an advanced player!", 2, "cena", 1, null, 1, null, 0 },
                    { "two", "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7803), "I want to player more than one game against the same adversary.", 2, "eltjo", 1, null, 1, "tijn", 1 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7754), "I wanna play a game and don't have any requirements!", 2, "karen", 2, null, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Board", "Date", "Draw", "Forfeit", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7863), false, false, "eltjo", "tijn" },
                    { "-1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7863), false, false, "eltjo", "tijn" },
                    { "-10", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7869), false, false, "john", "eltjo" },
                    { "-11", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7870), false, false, "ernst", "john" },
                    { "-12", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7870), false, false, "john", "cena" },
                    { "-13", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7871), false, false, "tijn", "burst" },
                    { "-14", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7871), false, false, "karen", "tijn" },
                    { "-15", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7872), false, false, "ernst", "burton" },
                    { "-16", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7873), false, false, "john", "burton" },
                    { "-17", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7873), false, false, "ernst", "cena" },
                    { "-18", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7874), false, false, "karen", "eltjo" },
                    { "-19", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7874), false, false, "cena", "burst" },
                    { "-2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7862), false, false, "tijn", "eltjo" },
                    { "-20", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7875), false, false, "burton", "ernst" },
                    { "-21", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7876), false, false, "john", "tijn" },
                    { "-22", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7876), false, false, "eltjo", "karen" },
                    { "-23", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7877), false, false, "burst", "john" },
                    { "-24", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7877), false, false, "tijn", "cena" },
                    { "-25", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7928), false, false, "burton", "karen" },
                    { "-26", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7929), false, false, "eltjo", "burst" },
                    { "-27", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7929), true, false, "pipo", "salie" },
                    { "-28", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7930), true, false, "gissa", "pipo" },
                    { "-29", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7930), false, false, "hidde", "gissa" },
                    { "-3", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7864), false, false, "ernst", "karen" },
                    { "-30", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7931), false, false, "noga", "hidde" },
                    { "-31", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7932), true, false, "nastrovia", "noga" },
                    { "-32", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7933), true, false, "pedro", "nastrovia" },
                    { "-33", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7933), false, false, "ahmed", "pedro" },
                    { "-34", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7934), false, false, "nadege", "ahmed" },
                    { "-35", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7935), true, false, "rachel", "nadege" },
                    { "-36", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7935), true, false, "salie", "rachel" },
                    { "-37", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7936), false, false, "hidde", "pipo" },
                    { "-38", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7936), false, false, "pedro", "noga" },
                    { "-39", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7937), false, false, "ahmed", "nastrovia" },
                    { "-4", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7866), false, false, "karen", "john" },
                    { "-40", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7937), false, false, "rachel", "pedro" },
                    { "-41", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7938), true, false, "pipo", "ahmed" },
                    { "-42", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7939), false, false, "nastrovia", "gissa" },
                    { "-43", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7939), false, false, "ahmed", "hidde" },
                    { "-44", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7940), false, false, "noga", "rachel" },
                    { "-45", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7940), false, false, "salie", "karen" },
                    { "-46", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7941), true, false, "pipo", "ernst" },
                    { "-47", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7942), false, false, "gissa", "john" },
                    { "-48", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7942), true, false, "hidde", "eltjo" },
                    { "-49", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7943), false, false, "noga", "tijn" },
                    { "-5", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7866), false, false, "john", "ernst" },
                    { "-50", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7943), false, false, "nastrovia", "cena" },
                    { "-51", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7944), true, false, "pedro", "burst" },
                    { "-52", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7945), false, false, "ahmed", "burton" },
                    { "-53", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7945), false, false, "nadege", "briar" },
                    { "-54", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7946), true, false, "rachel", "lambert" },
                    { "-55", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7946), false, false, "salie", "identity" },
                    { "-56", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7947), false, false, "ernst", "pipo" },
                    { "-57", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7948), true, false, "john", "gissa" },
                    { "-58", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7948), false, false, "tijn", "hidde" },
                    { "-59", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7949), false, false, "burst", "noga" },
                    { "-6", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7867), false, false, "karen", "cena" },
                    { "-60", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7949), false, false, "burton", "nastrovia" },
                    { "-61", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7950), true, false, "briar", "pedro" },
                    { "-62", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7951), false, false, "identity", "ahmed" },
                    { "-63", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7951), true, false, "lambert", "nadege" },
                    { "-64", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7952), false, false, "john", "rachel" },
                    { "-65", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7952), true, false, "cena", "karen" },
                    { "-66", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7953), false, false, "tijn", "burst" },
                    { "-67", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7954), false, false, "burton", "ernst" },
                    { "-68", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7954), true, false, "lambert", "john" },
                    { "-69", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7955), false, false, "gissa", "identity" },
                    { "-7", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7867), false, false, "burst", "karen" },
                    { "-70", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7955), true, false, "ernst", "salie" },
                    { "-71", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7956), false, false, "john", "pipo" },
                    { "-72", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7957), false, false, "eltjo", "hidde" },
                    { "-73", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7957), true, false, "burst", "nastrovia" },
                    { "-74", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7958), false, false, "briar", "nadege" },
                    { "-75", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7958), false, false, "tijn", "noga" },
                    { "-76", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7959), false, false, "lambert", "pedro" },
                    { "-77", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7960), true, false, "cena", "ahmed" },
                    { "-78", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7960), false, false, "burton", "rachel" },
                    { "-79", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7961), true, false, "noga", "tijn" },
                    { "-8", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7868), false, false, "cena", "ernst" },
                    { "-80", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7961), false, false, "pedro", "burton" },
                    { "-9", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7868), false, false, "eltjo", "tijn" },
                    { "0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7861), false, false, "eltjo", "tijn" },
                    { "1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7861), false, false, "eltjo", "tijn" },
                    { "2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 9, 19, 19, 43, 805, DateTimeKind.Utc).AddTicks(7859), false, false, "tijn", "eltjo" }
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
