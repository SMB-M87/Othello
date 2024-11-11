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
                    { "58a479fd-ae6f-4474-a147-68cbdb62c19b", "[]", false, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3433), "[]", "admin" },
                    { "ahmed", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3430), "[]", "Ahmed" },
                    { "briar", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3423), "[]", "Briar" },
                    { "burst", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3421), "[]", "Burst" },
                    { "burton", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3422), "[]", "Burton" },
                    { "cena", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3420), "[]", "Cena" },
                    { "deleted", "[]", false, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3434), "[]", "Deleted" },
                    { "eltjo", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3419), "[]", "Eltjo" },
                    { "ernst", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3417), "[]", "Ernst" },
                    { "ff20c418-f1b0-4f16-b582-294be25c24ef", "[]", false, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3432), "[]", "mediator" },
                    { "gissa", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3426), "[]", "Gissa" },
                    { "hidde", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3427), "[]", "Hidde" },
                    { "identity", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3424), "[]", "Identity" },
                    { "john", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3418), "[]", "John" },
                    { "karen", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3414), "[]", "Karen" },
                    { "lambert", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3423), "[]", "Lambert" },
                    { "nadege", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3431), "[]", "Nadege" },
                    { "nastrovia", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3428), "[]", "Nastrovia" },
                    { "noga", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3428), "[]", "Noga" },
                    { "pedro", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3429), "[]", "Pedro" },
                    { "pipo", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3426), "[]", "Pipo" },
                    { "rachel", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3431), "[]", "Rachel" },
                    { "salie", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3425), "[]", "Salie" },
                    { "tijn", "[]", true, new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3420), "[]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Board", "Date", "Draw", "Forfeit", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3567), false, false, "eltjo", "tijn" },
                    { "-1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3567), false, false, "eltjo", "tijn" },
                    { "-10", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3608), false, false, "john", "eltjo" },
                    { "-11", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3609), false, false, "ernst", "john" },
                    { "-12", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3609), false, false, "john", "cena" },
                    { "-13", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3610), false, false, "tijn", "burst" },
                    { "-14", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3612), false, false, "karen", "tijn" },
                    { "-15", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3612), false, false, "ernst", "burton" },
                    { "-16", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3613), false, false, "john", "burton" },
                    { "-17", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3613), false, false, "ernst", "cena" },
                    { "-18", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3614), false, false, "karen", "eltjo" },
                    { "-19", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3615), false, false, "cena", "burst" },
                    { "-2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3566), false, false, "tijn", "eltjo" },
                    { "-20", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3615), false, false, "burton", "ernst" },
                    { "-21", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3616), false, false, "john", "tijn" },
                    { "-22", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3616), false, false, "eltjo", "karen" },
                    { "-23", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3617), false, false, "burst", "john" },
                    { "-24", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3617), false, false, "tijn", "cena" },
                    { "-25", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3618), false, false, "burton", "karen" },
                    { "-26", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3619), false, false, "eltjo", "burst" },
                    { "-27", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3619), true, false, "pipo", "salie" },
                    { "-28", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3620), true, false, "gissa", "pipo" },
                    { "-29", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3620), false, false, "hidde", "gissa" },
                    { "-3", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3568), false, false, "ernst", "karen" },
                    { "-30", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3621), false, false, "noga", "hidde" },
                    { "-31", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3622), true, false, "nastrovia", "noga" },
                    { "-32", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3622), true, false, "pedro", "nastrovia" },
                    { "-33", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3623), false, false, "ahmed", "pedro" },
                    { "-34", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3623), false, false, "nadege", "ahmed" },
                    { "-35", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3624), true, false, "rachel", "nadege" },
                    { "-36", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3624), true, false, "salie", "rachel" },
                    { "-37", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3625), false, false, "hidde", "pipo" },
                    { "-38", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3626), false, false, "pedro", "noga" },
                    { "-39", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3626), false, false, "ahmed", "nastrovia" },
                    { "-4", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3569), false, false, "karen", "john" },
                    { "-40", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3627), false, false, "rachel", "pedro" },
                    { "-41", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3627), true, false, "pipo", "ahmed" },
                    { "-42", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3628), false, false, "nastrovia", "gissa" },
                    { "-43", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3629), false, false, "ahmed", "hidde" },
                    { "-44", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3629), false, false, "noga", "rachel" },
                    { "-45", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3630), false, false, "salie", "karen" },
                    { "-46", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3630), true, false, "pipo", "ernst" },
                    { "-47", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3631), false, false, "gissa", "john" },
                    { "-48", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3631), true, false, "hidde", "eltjo" },
                    { "-49", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3632), false, false, "noga", "tijn" },
                    { "-5", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3569), false, false, "john", "ernst" },
                    { "-50", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3633), false, false, "nastrovia", "cena" },
                    { "-51", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3633), true, false, "pedro", "burst" },
                    { "-52", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3634), false, false, "ahmed", "burton" },
                    { "-53", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3634), false, false, "nadege", "briar" },
                    { "-54", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3635), true, false, "rachel", "lambert" },
                    { "-55", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3636), false, false, "salie", "identity" },
                    { "-56", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3636), false, false, "ernst", "pipo" },
                    { "-57", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3637), true, false, "john", "gissa" },
                    { "-58", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3637), false, false, "tijn", "hidde" },
                    { "-59", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3638), false, false, "burst", "noga" },
                    { "-6", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3570), false, false, "karen", "cena" },
                    { "-60", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3638), false, false, "burton", "nastrovia" },
                    { "-61", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3639), true, false, "briar", "pedro" },
                    { "-62", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3640), false, false, "identity", "ahmed" },
                    { "-63", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3640), true, false, "lambert", "nadege" },
                    { "-64", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3641), false, false, "john", "rachel" },
                    { "-65", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3642), true, false, "cena", "karen" },
                    { "-66", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3642), false, false, "tijn", "burst" },
                    { "-67", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3643), false, false, "burton", "ernst" },
                    { "-68", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3643), true, false, "lambert", "john" },
                    { "-69", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3644), false, false, "gissa", "identity" },
                    { "-7", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3606), false, false, "burst", "karen" },
                    { "-70", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3644), true, false, "ernst", "salie" },
                    { "-71", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3645), false, false, "john", "pipo" },
                    { "-72", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3646), false, false, "eltjo", "hidde" },
                    { "-73", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3646), true, false, "burst", "nastrovia" },
                    { "-74", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3647), false, false, "briar", "nadege" },
                    { "-75", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3648), false, false, "tijn", "noga" },
                    { "-76", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3648), false, false, "lambert", "pedro" },
                    { "-77", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3649), true, false, "cena", "ahmed" },
                    { "-78", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3649), false, false, "burton", "rachel" },
                    { "-79", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3650), true, false, "noga", "tijn" },
                    { "-8", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3607), false, false, "cena", "ernst" },
                    { "-80", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3650), false, false, "pedro", "burton" },
                    { "-9", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3608), false, false, "eltjo", "tijn" },
                    { "0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3565), false, false, "eltjo", "tijn" },
                    { "1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3564), false, false, "eltjo", "tijn" },
                    { "2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 20, 6, 21, 94, DateTimeKind.Utc).AddTicks(3563), false, false, "tijn", "eltjo" }
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
